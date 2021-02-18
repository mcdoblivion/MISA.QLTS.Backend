using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common.Models
{
    /// <summary>
    /// Tài sản
    /// </summary>
    /// CreatedBy: DMCUONG (18/02/2021)
    public class Asset
    {
        #region Properties

        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public Guid? AssetTypeId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTime? IncreaseDate { get; set; }

        /// <summary>
        /// Thời gian sử dụng (năm)
        /// </summary>
        public int? TimeUse { get; set; }

        /// <summary>
        /// Tỉ lệ hào mòn
        /// </summary>
        public int? WearRate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public int? OriginalPrice { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public int? WearValue { get; set; }

        /// <summary>
        /// Sử dụng (true - sử dụng; false - không sử dụng)
        /// </summary>
        public bool IsUsed { get; set; }

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