using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZooMVC.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public abstract class BaseAdminController : Controller
    {
    }
}
