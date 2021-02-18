using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Models;

namespace MISA.DataLayer.Interfaces
{
    public interface IAssetDbContext : IDbContext<Asset>
    {
        /// <summary>
        /// Kiểm tra mã tài sản đã tồn tại trong database chưa
        /// </summary>
        /// <param name="assetCode">Mã tài sản cần kiểm tra</param>
        /// <returns>Mã tài sản/null</returns>
        /// CreatedBy: DMCUONG (18/02/2021)
        string CheckAssetCodeExist(string assetCode);
    }
}