using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Repository
{
    public interface ILicenseRepository : IBaseRepository<License>
    {
        #region Properties
        /// <summary>
        /// Kiểm tra tồn tại mã chứng từ
        /// </summary>
        /// <param name="licenseCode">Mã chứng từ đầu vào</param>
        /// <returns>true đã tồn tại mã chứng từ; false không tồn tại</returns>
        /// NDHoang(26/06/2022)
        bool CheckLicenseCodeExis(string licenseCode);

        /// <summary>
        /// Kiểm tra tồn tại mã chứng từ
        /// </summary>
        /// <param name="licenseCode">Mã chứng từ</param>
        /// <param name="licenseId">id chứng từ</param>
        /// <returns>true đã tồn tại mã chứng từ; false không tồn tại</returns>
        bool CheckUpdateLicenseCodeExis(string licenseCode, Guid licenseId);



        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageIndex">Chỉ mục trang hiện tại</param>
        /// <param name="pageSize">Tổng số chứng từ trên 1 trang</param>
        /// <param name="filter"></param>
        /// <returns>Danh danh sách chứng từ với số trang tương tứng</returns>
        /// NDHoang(26/06/2022)
        object GetPaging(int pageIndex, int pageSize, string? filter = "");
        #endregion

    }
}
