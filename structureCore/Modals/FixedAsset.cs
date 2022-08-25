namespace MISA.Web05
{
    /// <summary>
    /// Tài sản
    /// </summary>
    /// NDHoang(26/06/2022)
    public class FixedAsset:BaseEntity
    {
        #region Constructor
        public FixedAsset()
        {
            this.FixedAssetId = Guid.NewGuid();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// NDHoang(26/06/2022)
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// NDHoang(26/06/2022)
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// NDHoang(26/06/2022)
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        /// NDHoang(26/06/2022)
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// NDHoang(26/06/2022)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// NDHoang(26/06/2022)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        /// NDHoang(26/06/2022)
        public Guid? FixedAssetCategoryId { get; set; }

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
        /// Ngày mua
        /// </summary>
        /// NDHoang(26/06/2022)
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// NDHoang(26/06/2022)
        public decimal? Cost { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        /// NDHoang(26/06/2022)
        public DateTime? ProductionDate { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        /// NDHoang(26/06/2022)
        public int? Quantity { get; set; }

        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        /// NDHoang(26/06/2022)
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        /// NDHoang(26/06/2022)
        public float? DepreciationValueYear { get; set; }
        #endregion
        #region import
        public bool? IsValidImport { get; set; } = true;
        public List<string> ListErrorImport { get; set; } = new List<string>();
        #endregion
    }
}
