namespace MISA.Web05
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    /// NDHoang(26/06/2022)
    public class Department:BaseEntity
    {
        #region Constructor
        public Department()
        {
            this.DepartmentId = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// NDHoang(26/06/2022)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// NDHoang(26/06/2022)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary
        /// NDHoang(26/06/2022)
        public string DepartmentName { get; set; }
        #endregion

        #region Methods

        #endregion

    }
}
