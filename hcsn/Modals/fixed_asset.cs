namespace hcsn.Modals
{
    /// <summary>
    /// Tài sản
    /// </summary>
    /// NDHoang(26/06/2022)
    public class fixed_asset:BaseEntity
    {
        #region Constructor
        public fixed_asset()
        {
            this.fixed_asset_id = Guid.NewGuid();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string fixed_asset_name { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? organization_id { get; set; }

        /// <summary>
        /// Mã đơn bị
        /// </summary>
        public string? organization_code { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? organization_name { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string fixed_asset_category_name { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime purchase_date { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal cost { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public int production_year { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        public float depreciation_rate { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi
        /// </summary>
        public int tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public int depreciation_value_year { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public int active { get; set; }
        #endregion

    }
}
