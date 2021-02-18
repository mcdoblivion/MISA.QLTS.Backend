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
        public AssetsController(IAssetService assetService) : base(assetService)
        {
        }
    }
}