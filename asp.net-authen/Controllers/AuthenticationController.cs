using Microsoft.AspNetCore.Mvc;

namespace asp.net_authen.Controllers
{

    [Route("auth")]
    public class AuthenticationController : Controller
    {
        [Route("login", Name = "login_index_page")]
        public IActionResult Index()
        {
            return View("Views/Login/Index.cshtml");
        }



        [HttpPost("login_data", Name = "auth_login_data")]
        public IActionResult actionLogin()
        {

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (var key in Request.Form.Keys)
            {
                data.Add(key, Request.Form[key]);
            }


            if (data["username"] == "admin" && data["password"] == "admin")
            {
                HttpContext.Session.SetInt32("auth", 1);
                return RedirectToRoute("home_index_page");
            }
            else
            {
                actionAlertData("login failed", "warning");
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }



        [Route("logout", Name = "login_clear_data")]
        public IActionResult actionLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToRoute("login_index_page");
        }

        public void actionAlertData(string msg, string status = "success")
        {
            // success , danger , warning
            TempData["alear"] = "true";
            TempData["msg"] = msg;
            TempData["status"] = status;
        }

    }
}
