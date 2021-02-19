using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.Common.Models;
using MISA.Service.Interfaces;

namespace MISA.QLTS.Api.Controllers
{
    public class AssetsController : BaseController<Asset>
    {
        #region Declare

        private readonly IAssetService _assetService;

        #endregion Declare

        #region Constructor

        public AssetsController(IAssetService assetService) : base(assetService)
        {
            _assetService = assetService;
        }

        #endregion Constructor

        #region Method

        /// <summary>
        /// Tìm kiếm tài sản theo tên hoặc mã tài sản
        /// </summary>
        /// <param name="keyWord">Từ khoá tìm kiếm</param>
        /// <param name="year">Năm cần lọc</param>
        /// <param name="assetTypeId">Loại tài sản cần lọc</param>
        /// <param name="departmentId">Phòng ban cần lọc</param>
        /// <returns>Dữ liệu</returns>
        /// CreatedBy: DMCUONG (19/02/2021)
        [HttpGet("search")]
        public IActionResult Search(string keyWord = null, int year = 0, string assetTypeId = null, string departmentId = null)
        {
            if (string.IsNullOrEmpty(keyWord)
                && year == 0
                && string.IsNullOrEmpty(assetTypeId)
                && string.IsNullOrEmpty(departmentId))
                return base.Get();

            var serviceResult = _assetService.Get(keyWord, year, assetTypeId, departmentId);
            var entities = serviceResult.Data as List<Asset>;
            return StatusCode(entities.Count > 0 ? 200 : 204, entities);
        }

        #endregion Method
    }
}