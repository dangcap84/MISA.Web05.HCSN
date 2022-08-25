using MISA.Web05.Core.Resources;
using MISA.Web05.Exceptions;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Services
{
    public class LicenseAddService : BaseService<LicenseAdd>, ILicenseAddService
    {
        ILicenseAddRepository _licenseAddRepository;
        ILicenseService _licenseService;
        ILicenseDetailRepository _licenseDetailRepository;

        public LicenseAddService(ILicenseAddRepository licenseInsertRepository, ILicenseService licenseService, ILicenseDetailRepository licenseDetailRepository) : base(licenseInsertRepository)
        {
            _licenseAddRepository = licenseInsertRepository;
            _licenseService = licenseService;
            _licenseDetailRepository = licenseDetailRepository;
        }

        public object Insert(LicenseAdd licenseInsert)
        {
            // Thực hiện thêm mới dữ liệu
            licenseInsert.LicenseId = Guid.NewGuid();
            var license = new License
            {
                LicenseId = licenseInsert.LicenseId,
                LicenseCode = licenseInsert.LicenseCode,
                UseDate = licenseInsert.UseDate,
                WriteUpdate = licenseInsert.WriteUpdate,
                Total = licenseInsert.Total,
                Description = licenseInsert.Description
            };
            var licenseCount = _licenseService.InsertService(license);
            var licenseDetailCount = _licenseDetailRepository.MultiInsert(licenseInsert.licenseDetails, licenseInsert.LicenseId);
            return new
            {
                masterRes = licenseCount,
                detailRes = licenseDetailCount
            };
        }

        /// <summary>
        /// Sửa bản ghi master - detail
        /// </summary>
        /// <param name="licenseInsert"></param>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        /// <exception cref="MISAValidateException"></exception>
        public object Update(Guid licenseId, LicenseAdd licenseInsert)
        {
            //Validate dữ liệu: 
            var isValid = UpdateValidate(licenseInsert);
            //Thực hiện thêm mới:
            if (isValid)
            {
                var res = _licenseAddRepository.UpdateLicenseAdd(licenseId, licenseInsert);
                return res;
            }
            else
            {
                throw new MISAValidateException(ErrorValidateMsgs);
            }
        }

        /// <summary>
        /// Validate dữ liệu đầu vào khi update
        /// </summary>
        /// <param name="fixedAsset"></param>
        /// <returns>True: Dữ liệu hợp lệ, False: Dữ liệu không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected override bool UpdateValidate(LicenseAdd licenseAdd)
        {
            //Check trùng mã tài sản
            if (_licenseAddRepository.CheckUpdateLicenseCodeExis(licenseAdd.LicenseCode, licenseAdd.LicenseId))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("LicenseCodeDuplicateValue"));
            }
            if (!NullValue(licenseAdd))
                IsValid = false;
            return IsValid;
        }

        /// <summary>
        /// Check null value
        /// </summary>
        /// <param name="fixedAsset">Tài sản đầu vào</param>
        /// <returns></returns>
        private bool NullValue(LicenseAdd licenseAdd)
        {
            //Bắt buộc nhập
            if (string.IsNullOrEmpty(licenseAdd.LicenseCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetCodeNullValue"));
            }
            return IsValid;
        }
    }
}
