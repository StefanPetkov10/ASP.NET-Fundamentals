using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SeminarHub.Controllers
{
    public class BaseController : Controller
    {

        protected string GetUserId()
        {
            string id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return id;
        }

        //protected bool IsGuidIdValid(string? id, ref Guid parsedGuidId)
        //{
        //    // Non-existing parameter in the URL
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return false;
        //    }

        //    // Invalid parameter in the URL
        //    bool isGuidValid = Guid.TryParse(id, out parsedGuidId);
        //    if (!isGuidValid)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
