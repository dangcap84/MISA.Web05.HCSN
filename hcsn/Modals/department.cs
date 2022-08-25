namespace hcsn.Modals
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    /// NDHoang(26/06/2022)
    public class department:BaseEntity
    {
        #region Constructor
        public department()
        {
            this.department_id = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        public Guid? organization_id { get; set; }
        #endregion

        #region Methods

        #endregion

    }
}
