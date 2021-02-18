using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using MISA.Common.Models;
using MISA.DataLayer.Interfaces;

namespace MISA.DataLayer.DbContexts
{
    public class AssetDbContext : DbContext<Asset>, IAssetDbContext
    {
        #region Method

        /// <summary>
        /// Kiểm tra mã tài sản đã tồn tại trong database chưa
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần kiểm tra</param>
        /// <returns>Mã tài sản/null</returns>
        /// CreatedBy: DMCUONG (18/02/2021)
        public string CheckAssetCodeExist(string assetCode)
        {
            var sql = $"SELECT AssetCode FROM Asset AS A WHERE A.AssetCode = '{assetCode}'";
            var assetCodeInDb = _dbConnection.Query<string>(sql).FirstOrDefault();
            return assetCodeInDb;
        }

        #endregion Method
    }
}