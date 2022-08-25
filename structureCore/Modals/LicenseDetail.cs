using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05
{
    public class LicenseDetail
    {
        #region Constructor
        public LicenseDetail()
        {
            this.LicenseDetailId = Guid.NewGuid();
        }
        #endregion
        #region properties
        /// <summary>
        /// Khóa chính của chi tiết chứng từ
        /// </summary>
        /// NDHoang(14/08/2022)
        public Guid LicenseDetailId { get; set; }
        /// <summary>
        /// Khóa chính chứng từ
        /// </summary>
        /// NDHoang(14/08/2022)
        public Guid? LicenseId { get; set; }
        /// <summary>
        /// Khóa chính của tài sản
        /// </summary>
        /// NDHoang(14/08/2022)
        public Guid? FixedAssetId { get; set; }
        /// <summary>
        /// Chi tiết nguồn chi phí
        /// </summary>
        /// NDHoang(14/08/2022)
        public string? DetailJson { get; set; }
        #endregion
    }
}
