using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Modals
{
    public class User
    {
        /// <summary>
        /// Tên tài khoản đăng nhập
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        public string Password { get; set; }
    }
}
