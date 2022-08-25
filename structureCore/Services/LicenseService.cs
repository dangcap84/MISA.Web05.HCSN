using MISA.Web05.Core.Resources;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Services
{
    public class LicenseService : BaseService<License>, ILicenseService
    {
        #region Constructor
        /// <summary>
        /// Khởi tạo biến sử dụng ILicenseRepository
        /// </summary>
        /// NDHoang(04/07/2022)
        ILicenseRepository _repository;
        public LicenseService(ILicenseRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Validate dữ liệu đầu vào 
        /// </summary>
        /// <param name="fixedAsset">Tài sản đầu vào</param>
        /// <returns>True: Dữ liệu hợp lệ, False: Dữ liệu không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected override bool Validate(License license)
        {
            //Check trùng mã tài sản
            if (_repository.CheckLicenseCodeExis(license.LicenseCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("LicenseCodeDuplicateValue"));
            }
            if (!NullValue(license))
                IsValid = false;
            return IsValid;
        }

        /// <summary>
        /// Validate dữ liệu đầu vào khi update
        /// </summary>
        /// <param name="fixedAsset"></param>
        /// <returns>True: Dữ liệu hợp lệ, False: Dữ liệu không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected override bool UpdateValidate(License license)
        {
            //Check trùng mã tài sản
            if (_repository.CheckUpdateLicenseCodeExis(license.LicenseCode, license.LicenseId))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("LicenseCodeDuplicateValue"));
            }
            if (!NullValue(license))
                IsValid = false;
            return IsValid;
        }

        /// <summary>
        /// Check null value
        /// </summary>
        /// <param name="fixedAsset">Tài sản đầu vào</param>
        /// <returns></returns>
        private bool NullValue(License license)
        {
            //Bắt buộc nhập
            if (string.IsNullOrEmpty(license.LicenseCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetCodeNullValue"));
            }
            return IsValid;
        }
        #endregion
    }
}
