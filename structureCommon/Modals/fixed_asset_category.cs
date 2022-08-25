namespace structureCommon.Modals
{
    /// <summary>
    /// Loại tài sản
    /// </summary>
    /// NDHoang(26/06/2022)
    public class fixed_asset_category:BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string fixed_asset_category_name { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? organization_id { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        public float depreciation_rate { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int life_time { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? description { get; set; }
        #endregion
    }
}
