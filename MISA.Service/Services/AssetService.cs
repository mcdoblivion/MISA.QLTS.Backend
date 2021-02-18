using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Models;
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

        //

        #endregion Method
    }
}