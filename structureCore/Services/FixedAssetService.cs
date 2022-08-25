using Microsoft.AspNetCore.Http;
using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using MISA.Web05.Services;
using OfficeOpenXml;
using MISA.Web05.Core.Resources;
using System.Text;

namespace MISA.Web05.Service
{
    public class FixedAssetService : BaseService<FixedAsset>,IFixedAssetService
    {
        #region Constructor
        /// <summary>
        /// Khởi tạo biến sử dụng IFixedAssetRepository
        /// </summary>
        /// NDHoang(04/07/2022)
        IFixedAssetRepository _repository;
        public FixedAssetService(IFixedAssetRepository repository) : base(repository)
        {
            _repository = repository;
        }

        #endregion
        #region Method
        /// <summary>
        /// Validate dữ liệu đầu vào 
        /// </summary>
        /// <param name="fixedAsset">Tài sản đầu vào</param>
        /// <returns>True: Dữ liệu hợp lệ, False: Dữ liệu không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected override bool Validate(FixedAsset fixedAsset)
        {
            //Check trùng mã tài sản
            if (_repository.CheckFixedAssetCodeExis(fixedAsset.FixedAssetCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetCodeDuplicateValue"));
            }
            if (!NullValue(fixedAsset)) 
                IsValid = false;
            if (!OverLength(fixedAsset))
                IsValid = false;
            return IsValid;
        }

        /// <summary>
        /// Validate dữ liệu đầu vào 
        /// </summary>
        /// <param name="fixedAsset">Tài sản đầu vào</param>
        /// <returns>True: Dữ liệu hợp lệ, False: Dữ liệu không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected override bool DeleteValidate(string? fixedAsset)
        {
            //Check trùng mã tài sản
            if (_repository.CheckLicenseExis(fixedAsset))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("LicenseFixedAssetValue"));
            }
            return IsValid;
        }



        /// <summary>
        /// Validate dữ liệu đầu vào khi update
        /// </summary>
        /// <param name="fixedAsset"></param>
        /// <returns>True: Dữ liệu hợp lệ, False: Dữ liệu không hợp lệ</returns>
        /// NDHoang(04/07/2022)
        protected override bool UpdateValidate(FixedAsset fixedAsset)
        {
            //Check trùng mã tài sản
            if (_repository.CheckUpdateFixedAssetCodeExis(fixedAsset.FixedAssetCode, fixedAsset.FixedAssetId))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetCodeDuplicateValue"));
            }
            if (!NullValue(fixedAsset)) 
                IsValid = false;
            if (!OverLength(fixedAsset))
                IsValid = false;
            return IsValid;
        }

        /// <summary>
        /// Check null value
        /// </summary>
        /// <param name="fixedAsset">Tài sản đầu vào</param>
        /// <returns></returns>
        private bool NullValue(FixedAsset fixedAsset)
        {
            //Bắt buộc nhập
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetCodeNullValue"));
            }

            if (string.IsNullOrEmpty(fixedAsset.DepartmentCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("DepartmentCodeNullValue"));
            }
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCategoryCode))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetCategoryCodeNullValue"));
            }
            if (string.IsNullOrEmpty((fixedAsset.Quantity).ToString()))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("QuantityNullValue"));
            }
            if (string.IsNullOrEmpty((fixedAsset.Cost).ToString()))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("CostNullValue"));
            }
            return IsValid;
        }

        /// <summary>
        /// Validate độ dài
        /// </summary>
        /// <param name="fixedAsset">Tài sản đầu vào</param>
        /// <returns></returns>
        private bool OverLength(FixedAsset fixedAsset)
        {
            if (fixedAsset.FixedAssetCode.Length > ConvertToInt(Resource.ResourceManager.GetString("CodeMaxLengthNumber")))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetCodeMaxLength"));
            }
            if (fixedAsset.FixedAssetName.Length > ConvertToInt(Resource.ResourceManager.GetString("NameMaxLengthNumber")))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("FixedAssetNameMaxLength"));
            }
            if (fixedAsset.Quantity > ConvertToFloat(Resource.ResourceManager.GetString("QuantityMaxLengthNumber")))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("QuantityMaxLength"));
            }
            if (fixedAsset.Cost > ConvertToDecimal(Resource.ResourceManager.GetString("CostMaxLengthNumber")))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("CostMaxLength"));
            }
            if (fixedAsset.DepreciationRate > ConvertToFloat(Resource.ResourceManager.GetString("DepreciationMaxLengthNumber")))
            {
                IsValid = false;
                ErrorValidateMsgs.Add(Resource.ResourceManager.GetString("DepreciationMaxLength"));
            }
            return IsValid;
        }

        /// <summary>
        /// Nhập khẩu file excel
        /// </summary>
        /// <param name="fileImport">file đầu vào</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// NDHoang(04/07/2022)
        public IEnumerable<FixedAsset> Import(IFormFile fileImport)
        {
            //validate
            if (!Path.GetExtension(fileImport.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Tệp không đúng định dạng");
            }
            //Khởi tạo đối tượng
            var fixedAssets = new List<FixedAsset>();

            using (var stream = new MemoryStream())
            {
                fileImport.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    //Lấy chỉ mục của excel
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    //Duyệt từng dòng
                    for (int row = 2; row <= rowCount; row++)
                    {
                        //lưu các giá trị
                        var fixedAssetCode = ConvertToString(worksheet.Cells[row, 2].Value);
                        var fixedAssetName = ConvertToString(worksheet.Cells[row, 3].Value);
                        var purchaseDate = ConvertToDate(worksheet.Cells[row, 4].Value);
                        var productionDate = ConvertToDate(worksheet.Cells[row, 5].Value);
                        var quantity = ConvertToInt(worksheet.Cells[row, 6].Value);
                        var cost = ConvertToDecimal(worksheet.Cells[row, 7].Value);
                        var fixedAssetCategoryCode = ConvertToString(worksheet.Cells[row, 8].Value);
                        var departmentCode = ConvertToString(worksheet.Cells[row, 9].Value);
                        var lifeTime = ConvertToInt(worksheet.Cells[row, 10].Value);
                        var depreciationRate = ConvertToFloat(worksheet.Cells[row, 11].Value);
                        var depreciationValueYear = ConvertToFloat(worksheet.Cells[row, 12].Value);
                        var departmentId = ConvertToGuid(worksheet.Cells[row, 13].Value);
                        var fixedAssetCategoryId = ConvertToGuid(worksheet.Cells[row, 14].Value);

                        //Tạo đối tượng
                        var fixedAsset = new FixedAsset
                        {
                            FixedAssetCode = fixedAssetCode,
                            FixedAssetName = fixedAssetName,
                            FixedAssetCategoryId = fixedAssetCategoryId,
                            FixedAssetCategoryCode = fixedAssetCategoryCode,
                            DepartmentId = departmentId,
                            DepartmentCode = departmentCode,
                            PurchaseDate = purchaseDate,
                            ProductionDate = productionDate,
                            Quantity = quantity,
                            Cost = cost,
                            LifeTime = lifeTime,
                            DepreciationRate = depreciationRate,
                            DepreciationValueYear = depreciationValueYear,
                        };
                        //Thực hiện validate dữ liệu:
                        //Xóa toàn bộ thông tin lỗi của object trước
                        ErrorValidateMsgs.Clear();
                        //Gán lại giá trị cho IsValid
                        IsValid = true;
                        var isValid = Validate(fixedAsset);
                        if (!isValid)
                        {
                            fixedAsset.IsValidImport = false;
                            fixedAsset.ListErrorImport.AddRange(ErrorValidateMsgs);
                        }
                        fixedAssets.Add(fixedAsset);
                    }
                }
            }
            //lấy danh sách tài sản hợp lệ
            var fixedAssetValids = fixedAssets.Where(e => e.IsValidImport == true).ToList();
            if (fixedAssetValids.Count() > 0)
            {
                var res = _repository.Import(fixedAssetValids);
            }
            return fixedAssets;
        }

        /// <summary>
        /// Convert dữ liệu sang string
        /// </summary>
        /// <param name="value">Đối số</param>
        /// <returns>Đối số kiểu string</returns>
        /// NDHoang(04/07/2022)
        private string? ConvertToString(object? value)
        {
            if (value != null)
                return value.ToString();
            else return null;
        }

        /// <summary>
        /// Convert dữ liệu sang decimal
        /// </summary>
        /// <param name="value">Đối số</param>
        /// <returns>Đối số kiểu decimal</returns>
        /// NDHoang(04/07/2022)
        private decimal? ConvertToDecimal(object? value)
        {
            Decimal decimalConvert;
            if (value != null && Decimal.TryParse(value.ToString(), out decimalConvert))
                return decimalConvert;
            else return null;
        }

        /// <summary>
        /// Convert dữ liệu sang date
        /// </summary>
        /// <param name="value">Đối số</param>
        /// <returns>Đối số kiểu int</returns>
        /// NDHoang(04/07/2022)
        private DateTime? ConvertToDate(object? value)
        {
            DateTime dateConvert;
            if (value != null && DateTime.TryParse(value.ToString(), out dateConvert))
            {
                return dateConvert;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert dữ liệu sang int
        /// </summary>
        /// <param name="value">Đối số</param>
        /// <returns>Đối số kiểu int</returns>
        /// NDHoang(04/07/2022)
        private int? ConvertToInt(object? value)
        {
            var number = ConvertToString(value);
            if (value != null)
                return int.Parse(number);
            else return null;
        }

        /// <summary>
        /// Convert dữ liệu sang float
        /// </summary>
        /// <param name="value">Đối số</param>
        /// <returns>Đối số kiểu int</returns>
        /// NDHoang(04/07/2022)
        private float? ConvertToFloat(object? value)
        {
            var number = ConvertToString(value);
            if (value != null)
                return float.Parse(number);
            else return null;
        }

        /// <summary>
        /// Convert dữ liệu sang guid
        /// </summary>
        /// <param name="value">Đối số</param>
        /// <returns>Đối số kiểu guid</returns>
        /// NDHoang(04/07/2022)
        private Guid? ConvertToGuid(object? value)
        {
            var guid = ConvertToString(value);
            if (value != null)
                return new Guid(guid);
            else return null;
        }
        #endregion
    }
}
