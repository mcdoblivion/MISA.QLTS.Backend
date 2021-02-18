using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common.Models
{
    /// <summary>
    /// Loại tài sản
    /// </summary>
    /// CreatedBy: DMCUONG (18/02/2021)
    public class AssetType
    {
        #region Properties

        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid AssetTypeId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string AssetTypeCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetTypeName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        #endregion Properties

        #region Other

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }

        #endregion Other
    }
}