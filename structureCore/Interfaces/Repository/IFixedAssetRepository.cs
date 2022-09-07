using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Repository
{
    public interface IFixedAssetRepository : IBaseRepository<FixedAsset>
    {
        #region Method
        /// <summary>
        /// Kiểm tra tồn tại mã tài sản
        /// </summary>
        /// <param name="fixedAssetCode"></param>
        /// <returns>true đã tồn tại mã tài sản; false không tồn tại</returns>
        /// NDHoang(26/06/2022)
        bool CheckFixedAssetCodeExis(string fixedAssetCode);

        /// <summary>
        /// Kiểm tra tồn tại chứng từ chưa
        /// </summary>
        /// <param name="fixedAssetId">id tài sản đầu vào</param>
        /// <returns>true đã tồn tại mã tài sản; false không tồn tại</returns>
        /// NDHoang(26/06/2022)
        bool CheckLicenseExis(string? fixedAssetId);
        
        /// <summary>
        /// Kiểm tra tồn tại mã tài sản
        /// </summary>
        /// <param name="fixedAssetCode">Mã tài sản</param>
        /// <param name="fixedAssetId">id tài sản</param>
        /// <returns>true đã tồn tại mã tài sản; false không tồn tại</returns>
        bool CheckUpdateFixedAssetCodeExis(string fixedAssetCode, Guid fixedAssetId);

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageIndex">Chỉ mục trang hiện tại</param>
        /// <param name="pageSize">Tổng số tài sản trên 1 trang</param>
        /// <param name="filter"></param>
        /// <returns>Danh danh sách tài sản với số trang tương tứng</returns>
        /// NDHoang(26/06/2022)
        object GetPaging(int pageIndex, int pageSize, string? filter = "", string? departmentName = "", string? categoryName = "");
        /// <summary>
        /// Phân trang loại bỏ tài sản đã chọn trong license
        /// </summary>
        /// <param name="pageIndex">Chỉ mục trang hiện tại</param>
        /// <param name="pageSize">Tổng số tài sản trên 1 trang</param>
        /// <param name="filter"></param>
        /// <returns>Danh danh sách tài sản với số trang tương tứng</returns>
        /// NDHoang(26/06/2022)
        object GetPagingNoFixedAsset(int pageIndex, int pageSize, Guid[] licenseIds, string? filter = "", string? departmentName = "", string? categoryName = "");
        /// <summary>
        /// Import
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản đầu vào</param>
        /// <returns></returns>
        /// NDHoang(26/06/2022)
        int Import(IEnumerable<FixedAsset> fixedAssets);
        #endregion
    }
}
