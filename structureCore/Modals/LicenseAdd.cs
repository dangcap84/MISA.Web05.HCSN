using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05
{
    public class LicenseAdd
    {
        #region Constructor
        public LicenseAdd()
        {
            this.LicenseId = Guid.NewGuid();
        }
        #endregion
        #region properties

        /// <summary>
        /// Khóa chính của chứng từ
        /// </summary>
        /// NDHoang(14/08/2022)
        public Guid LicenseId { get; set; }
        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// NDHoang(14/08/2022)
        public string? LicenseCode { get; set; }
        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        /// NDHoang(14/08/2022)
        public DateTime? UseDate { get; set; }
        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        /// NDHoang(14/08/2022)
        public DateTime? WriteUpdate { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        /// NDHoang(14/08/2022)
        public string? Description { get; set; }
        /// <summary>
        /// Tổng nguyên giá
        /// </summary>
        /// NDHoang(14/08/2022)
        public float? Total { get; set; }

        /// <summary>
        /// Danh sách tài sản ở bảng thông tin chi tiết
        /// </summary>
        public LicenseDetail[] licenseDetails { get; set; }
        #endregion
    }
}
