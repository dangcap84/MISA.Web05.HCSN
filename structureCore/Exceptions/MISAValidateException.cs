using System.Collections;

namespace MISA.Web05.Exceptions
{
    public class MISAValidateException : Exception
    {
        #region Constructor
        public MISAValidateException(string ErrorMsg)
        {
            validateErrorMsg = ErrorMsg;
        }

        /// <summary>
        /// Ghi đè constructor
        /// </summary>
        /// <param name="errorMsg">Thông tin lỗi</param>
        /// NDHoang(26/06/2022)
        public MISAValidateException(List<string> errorMsg)
        {
            Errors = new Dictionary<string, object>();
            Errors.Add("Error", errorMsg);
        }
        #endregion
        #region Properties
        ///Tạo biến
        public string validateErrorMsg { get; set; }
        public IDictionary Errors { get; set; }

        /// Gán giá trị
        public override string Message => validateErrorMsg;
        public override IDictionary Data => Errors;
        #endregion

    }
}
