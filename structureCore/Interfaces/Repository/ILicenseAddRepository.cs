using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Interfaces.Repository
{
    public interface ILicenseAddRepository : IBaseRepository<LicenseAdd>
    {
        #region Properties
        /// <summary>
        /// Kiểm tra tồn tại mã tài sản
        /// </summary>
        /// <param name="fixedAssetCode">Đối tượng tham chiếu</param>
        /// <param name="fixedAssetId">Đối tượng tham chiếu</param>
        /// <returns>false: không tồn tại mã, true: đã có mã</returns>
        /// NDHoang(04/07/2022)
        public bool CheckUpdateLicenseCodeExis(string licenseCode, Guid licenseId);

        /// <summary>
        /// Thêm 1 mảng license detail
        /// </summary>
        /// <param name="licenseDetails"></param>
        /// <returns></returns>
        object MultiAdd(LicenseAdd licenseInsert);

        /// <summary>
        /// Sửa bản ghi master - detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <returns></returns>
        object UpdateLicenseAdd(Guid licenseId, LicenseAdd licenseInsert);
        #endregion
    }
}
