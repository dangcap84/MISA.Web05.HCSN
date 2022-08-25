using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core.Modals;
using MISA.Web05.Core.Resources;
using MISA.Web05.Exceptions;
using System.Security.Claims;

namespace MISA.Web05.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Hàm xử lý login, tạo cookie nắm giữ thông tin người dùng 
        /// </summary>
        /// <param name="userLogin">Thông tin tài khoản, mật khẩu đầu vào</param>
        /// <returns>Ok hoặc lỗi sai tài khoản mật khẩu</returns>
        /// NDHoang(09/08/2022)
        [AllowAnonymous] //Sử dụng thuộc tính AllowAnonymous để cho phép người dùng chưa xác thực truy cập vào hành động đăng nhập
        [HttpPost]
        public async Task<IActionResult> OnPostAsync([FromBody] User userLogin)
        {
            try
            {
                //Gọi hàm AuthenticateUser được viết phía dưới dùng để xác thực thông tin đăng nhập
                var user = AuthenticateUser(userLogin.UserName, userLogin.Password);
                //Nếu user trả về null nghĩa là thông tin đăng nhập sai
                if (user == null)
                {
                    return StatusCode(200, -1);
                }
                // Tạo danh sách claim
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };
                // Tạo Identity
                // Một Identity có thể có nhiều claim
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                // Tạo Principal
                // Đăng nhập một tài khoản chính có tất cả thông tin của người dùng bằng SignInAsync
                // Một Principal có thể chứa nhiều Identity
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),null);
                return Ok();
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        /// <summary>
        /// Check username, password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User đúng tài khoản mật khẩu hoặc null</returns>
        /// NDhoang(09/08/2022)
        [AllowAnonymous]
        private User AuthenticateUser(string username, string password)
        {
            try
            {
                // Hash mật khẩu trước khi so sánh
                if (username == "ndhoang2001" && password == "1")
                {
                    return new User()
                    {
                        UserName = "ndhoang2001",
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return (User)HandleException(ex);
            }
        }
        /// <summary>
        /// Hàm logout, xóa cookie
        /// </summary>
        /// <returns>OK</returns>
        /// NDHoang(09/08/2022)

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut , clear cookies
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            //return RedirectToAction("index")''
            return Ok();
        }
        private IActionResult HandleException(Exception ex)
        {
            // Nếu ex thuộc validate được viết ở trên
            if (ex is MISAValidateException)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    userMsg = ex.Message,
                };
                return StatusCode(401, res);
            }
            else {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Resource.ResourceManager.GetString("MisaError"),
                };
                return StatusCode(500, res);
            }
        }
    }
}
