using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MISA.Common.Models;
using MISA.Common.Properties;
using MISA.DataLayer.Interfaces;
using MISA.Service.Interfaces;

namespace MISA.Service.Services
{
    /// <summary>
    /// Xử lý nghiệp vụ tài sản
    /// </summary>
    /// CreatedBy: DMCUONG (18/02/2021)
    public class AssetService : BaseService<Asset>, IAssetService
    {
        #region Declare

        private readonly IAssetDbContext _dbContext;

        #endregion Declare

        #region Constructor

        public AssetService(IAssetDbContext assetDbContext) : base(assetDbContext)
        {
            _dbContext = assetDbContext;
        }

        #endregion Constructor

        #region Method

        protected override bool IsDataExist(string id, ErrorMsg errorMsg = null)
        {
            return base.IsDataExist(id, errorMsg);
        }

        protected override bool IsInsertDataValid(Asset asset, ErrorMsg errorMsg = null)
        {
            var isValid = true;
            if (errorMsg == null) errorMsg = new ErrorMsg();

            // Validate dữ liệu(xử lý nghiệp vụ)
            // 1. Bắt buộc nhập
            // - Mã tài sản
            if (string.IsNullOrEmpty(asset.AssetCode))
            {
                isValid = false;
                errorMsg.UserMsg.Add(Resources.ErrorService_EmptyAssetCode);
            }

            // - Tên tài sản
            if (string.IsNullOrEmpty(asset.AssetName))
            {
                isValid = false;
                errorMsg.UserMsg.Add(Resources.ErrorService_EmptyAssetName);
            }

            // 2. Không được phép trùng
            // - Mã tài sản
            var assetCodeExist = _dbContext.CheckAssetCodeExist(asset.AssetCode);
            if (assetCodeExist != null)
            {
                isValid = false;
                errorMsg.UserMsg.Add(Resources.ErrorService_DuplicateAssetCode);
            }

            return isValid;
        }

        protected override bool IsUpdateDataValid(string id, Asset asset, ErrorMsg errorMsg = null)
        {
            var isValid = true;
            if (errorMsg == null) errorMsg = new ErrorMsg();

            // Kiểm tra tài sản có tồn tại trong database không
            if (!IsDataExist(id, errorMsg))
            {
                isValid = false;
                errorMsg.UserMsg.Add(Resources.ErrorService_IdNotExist + $": {id}");
                return isValid;
            }

            // Nếu tồn tại thì validate dữ liệu(xử lý nghiệp vụ)
            // 1. Bắt buộc nhập
            // - Mã tài sản
            if (string.IsNullOrEmpty(asset.AssetCode))
            {
                isValid = false;
                errorMsg.UserMsg.Add(Resources.ErrorService_EmptyAssetCode);
            }

            // - Tên tài sản
            if (string.IsNullOrEmpty(asset.AssetName))
            {
                isValid = false;
                errorMsg.UserMsg.Add(Resources.ErrorService_EmptyAssetName);
            }

            // 2. Không được phép trùng
            // - Mã tài sản
            var assetCodeExist = _dbContext.CheckAssetCodeExist(asset.AssetCode);
            var oldAssetCode = _dbContext.GetObject(sqlCommand: $"SELECT * FROM Asset WHERE AssetId = '{id}'").FirstOrDefault().AssetCode;
            if (assetCodeExist != null && assetCodeExist != oldAssetCode)
            {
                isValid = false;
                errorMsg.UserMsg.Add(Resources.ErrorService_DuplicateAssetCode);
            }

            return isValid;
        }

        public ServiceResult Get(string keyWord = null, int year = 0, string assetTypeId = null, string departmentId = null)
        {
            if (string.IsNullOrEmpty(keyWord)
                && year == 0
                && string.IsNullOrEmpty(assetTypeId)
                && string.IsNullOrEmpty(departmentId))
                return base.Get();

            var condition = string.Empty;

            if (!string.IsNullOrEmpty(keyWord))
                condition += $"WHERE (AssetName LIKE '%{keyWord}%' OR AssetCode LIKE '%{keyWord}%')";

            if (year > 0)
            {
                if (string.IsNullOrEmpty(condition))
                    condition += " WHERE ";
                else
                    condition += " AND ";

                condition += $"YEAR(IncreaseDate) = '{year}'";
            }

            if (!string.IsNullOrEmpty(assetTypeId))
            {
                if (string.IsNullOrEmpty(condition))
                    condition += " WHERE ";
                else
                    condition += " AND ";

                condition += $"AssetTypeId = '{assetTypeId}'";
            }

            if (!string.IsNullOrEmpty(departmentId))
            {
                if (string.IsNullOrEmpty(condition))
                    condition += " WHERE ";
                else
                    condition += " AND ";

                condition += $"DepartmentId = '{departmentId}'";
            }

            var sql = $"SELECT * FROM Asset {condition}";
            var serviceResult = new ServiceResult()
            {
                Data = _dbContext.GetObject(sqlCommand: sql)
            };
            return serviceResult;
        }

        #endregion Method
    }
}