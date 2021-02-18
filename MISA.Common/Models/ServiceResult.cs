using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common.Models
{
    /// <summary>
    /// Kết quả xử lý nghiệp vụ
    /// </summary>
    /// CreatedBy: DMCUONG (05/02/2021)
    public class ServiceResult
    {
        #region Constructor

        public ServiceResult()
        {
            Success = true;
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// Trạng thái service: true - thành công, false - thất bại
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Dữ liệu trả về sau khi xử lý nghiệp vụ
        /// </summary>
        public object Data { get; set; }

        public string MISACode { get; set; }

        #endregion Property
    }
}