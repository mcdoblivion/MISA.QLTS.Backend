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
    [Route("api/v1/asset-types")]
    public class AssetTypesController : BaseController<AssetType>
    {
        #region Constructor

        public AssetTypesController(IBaseService<AssetType> baseService) : base(baseService)
        {
        }

        #endregion Constructor
    }
}