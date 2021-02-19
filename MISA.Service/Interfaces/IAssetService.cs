using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Models;

namespace MISA.Service.Interfaces
{
    public interface IAssetService : IBaseService<Asset>
    {
        /// <summary>
        /// Tìm kiếm và lọc tài sản theo nhiều tiêu chí
        /// </summary>
        /// <param name="keyWord">Từ khoá tìm kiếm</param>
        /// <param name="year">Năm</param>
        /// <param name="assetTypeId">Loại tài sản</param>
        /// <param name="departmentId">Phòng ban</param>
        /// <returns>ServiceResult</returns>
        ServiceResult Get(string keyWord = null, int year = 0, string assetTypeId = null, string departmentId = null);
    }
}