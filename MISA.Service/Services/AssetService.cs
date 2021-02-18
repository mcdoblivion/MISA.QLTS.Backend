﻿using System;
using System.Collections.Generic;
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

        protected override bool IsUpdateDataValid(Asset entity, ErrorMsg errorMsg = null)
        {
            return base.IsUpdateDataValid(entity, errorMsg);
        }

        #endregion Method
    }
}