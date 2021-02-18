using System;
using System.Collections.Generic;
using System.Text;
using MISA.Common.Models;
using MISA.DataLayer.Interfaces;

namespace MISA.DataLayer.DbContexts
{
    public class AssetDbContext : DbContext<Asset>, IAssetDbContext
    {
    }
}