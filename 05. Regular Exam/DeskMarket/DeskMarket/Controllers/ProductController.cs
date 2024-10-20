using System.Globalization;
using System.Security.Claims;
using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models;
using GameZone.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DeskMarket.Common.ValidationConstants.Product;

namespace DeskMarket.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ApplicationDbContext data;

        public ProductController(ApplicationDbContext context)
        {
            data = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await data.Products
                .Where(p => p.IsDeleted == false)
                .Select(p => new AllProductViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ImageUrl = p.ImageUrl!,
                    Price = p.Price
                })
                .AsNoTracking()
                .ToArrayAsync();

            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await data.Categories
                .Select(g => new CategoryViewModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .AsNoTracking()
                .ToArrayAsync();

            var model = new AddProductViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            DateTime date = DateTime.UtcNow;

            if (!DateTime.TryParseExact(model.AddedOn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                ModelState
                   .AddModelError(nameof(model.AddedOn), $"Invalid date! Format must be: {AddedOnFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var product = new DeskMarket.Data.Models.Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                AddedOn = date,
                CategoryId = model.CategoryId,
                SellerId = GetUserId()
            };

            data.Products.AddAsync(product);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int Id, AddCartViewModel model)
        {
            var p = await data.Products
                .Where(e => e.Id == Id)
                .Include(e => e.ProductsClients)
                .FirstOrDefaultAsync();

            if (p == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!p.ProductsClients.Any(c => c.ClientId == userId))
            {
                p.ProductsClients.Add(new ProductClient()
                {
                    ProductId = p.Id,
                    ClientId = userId
                });

                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            var model = await data.ProductsClients
                .Where(ep => ep.ClientId == userId)
                .AsNoTracking()
                .Select(b => new AddCartViewModel
                {
                    Id = b.ProductId,
                    ProductName = b.Product.ProductName,
                    ImageUrl = b.Product.ImageUrl,
                    Price = b.Product.Price
                })
                .ToListAsync();

            return View(model);
        }


        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var p = await data.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductsClients)
                .FirstOrDefaultAsync();

            if (p == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = p.ProductsClients
                .FirstOrDefault(ep => ep.ClientId == userId);

            if (ep == null)
            {
                return BadRequest();
            }

            p.ProductsClients.Remove(ep);

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Products
                .Where(p => p.Id == id && !p.IsDeleted)
                .Select(p => new DetailsViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    CategoryName = p.Category.Name,
                    AddedOn = p.AddedOn.ToString(AddedOnFormat),
                    Seller = p.Seller.UserName,
                    HasBought = p.ProductsClients.Any()
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return RedirectToAction("Index", "Product");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await data.Products
                .FindAsync(id);

            if (p == null)
            {
                return BadRequest();
            }

            if (p.SellerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new EditProductViewModel()
            {
                ProductName = p.ProductName,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                AddedOn = p.AddedOn.ToString(AddedOnFormat),
                CategoryId = p.CategoryId,
                SellerId = p.SellerId
            };

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel model)
        {
            var p = await data.Products
                .FindAsync(id);

            if (p == null)
            {
                return BadRequest();
            }

            if (p.SellerId != GetUserId())
            {
                return Unauthorized();
            }


            DateTime date = DateTime.UtcNow;

            if (!DateTime.TryParseExact(model.AddedOn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                ModelState
                   .AddModelError(nameof(model.AddedOn), $"Invalid date! Format must be: {AddedOnFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            p.ProductName = model.ProductName;
            p.Description = model.Description;
            p.Price = model.Price;
            p.ImageUrl = model.ImageUrl;
            p.AddedOn = date;
            p.CategoryId = model.CategoryId;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await data.Products
                .Where(p => p.Id == id)
                .Select(p => new DeleteProductViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    SellerId = p.SellerId,
                    Seller = p.Seller.UserName
                })
                .FirstOrDefaultAsync();

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeleteProductViewModel model)
        {
            var product = await data.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                product.IsDeleted = true;
                await data.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Product");
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
