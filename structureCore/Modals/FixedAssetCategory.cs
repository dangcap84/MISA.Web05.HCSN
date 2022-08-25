namespace MISA.Web05
{
    /// <summary>
    /// Loại tài sản
    /// </summary>
    /// NDHoang(26/06/2022)
    public class FixedAssetCategory:BaseEntity
    {
        #region Constructor
        public FixedAssetCategory()
        {
            this.FixedAssetCategoryId = Guid.NewGuid();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// NDHoang(26/06/2022)
        public Guid FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// NDHoang(26/06/2022)
        public string FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// NDHoang(26/06/2022)
        public string FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// NDHoang(26/06/2022)
        public int? LifeTime { get; set; }

        #endregion
    }
}
