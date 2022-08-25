using MISA.Web05.Core.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Repository
{
    public interface ILicenseDetailRepository : IBaseRepository<LicenseDetail>
    {

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageIndex">Chỉ mục trang hiện tại</param>
        /// <param name="pageSize">Tổng số chứng từ trên 1 trang</param>
        /// <param name="filter"></param>
        /// <returns>Danh danh sách chứng từ với số trang tương tứng</returns>
        /// NDHoang(26/06/2022)
        object GetPaging(int pageIndex, int pageSize, string? filter = "", string licenseId = "");

        /// <summary>
        /// Thêm 1 mảng license detail
        /// </summary>
        /// <param name="licenseDetails"></param>
        /// <returns></returns>
        object MultiInsert(LicenseDetail[] licenseDetails, Guid licenseId);
        /// <summary>
        /// Lấy ra 1 licenseInsert, vừa chứa master, vừa chứa detail
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        object GetLicenseInsertById(Guid licenseId);
        /// <summary>
        /// Lấy ra list detail từ 1 licenseId
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        List<object> GetDetailAssets(Guid licenseId);
        /// <summary>
        /// Sửa bản license detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        int UpdateLicenseDetail(Guid licenseDetailId, string detailJson);
        /// <summary>
        /// Lấy ra bộ phận sử dụng và detail json của license detail
        /// </summary>
        /// <param name="licenseDetailId"></param>
        /// <returns></returns>
        object GetMoneySource(Guid licenseDetailId);
    }
}
