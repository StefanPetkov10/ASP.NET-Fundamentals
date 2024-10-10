using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsGuidIdValid(string? id, ref Guid cinemaGuidId)
        {
            // Non-existing parameter in the URL
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            // Invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(id, out cinemaGuidId);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
