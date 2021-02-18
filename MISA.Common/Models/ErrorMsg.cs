using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common.Models
{
    /// <summary>
    /// Thông báo lỗi
    /// </summary>
    /// CreatedBy: DMCUONG (05/02/2021)
    public class ErrorMsg
    {
        /// <summary>
        /// Thông báo cho developer
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public List<string> UserMsg { get; set; } = new List<string>();

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Trace ID
        /// </summary>
        public string TraceId { get; set; }
    }
}