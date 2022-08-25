namespace MISA.Web05
{
    public class BaseEntity
    {
        /// <summary>
        /// Người tạo
        /// </summary>
        /// NDHoang(26/06/2022)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// NDHoang(26/06/2022)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// NDHoang(26/06/2022)
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// NDHoang(26/06/2022)
        public DateTime? ModifiedDate { get; set; }
    }
}
