using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.DataLayer.Interfaces
{
    /// <summary>
    /// Interface ngắt sự phụ thuộc của Service vào DataLayer
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// CreatedBy: DMCUONG(08/02/2021)
    public interface IDbContext<TEntity>
    {
        /// <summary>
        /// Lấy dữ liệu theo nhiều tiêu chí khác nhau
        /// </summary>
        /// <typeparam name="TEntity">Type của object</typeparam>
        /// <param name="sqlCommand">sql command (không truyền: lấy tất cả)</param>
        /// <param name="parameters">Đối tượng chứa thông tin tham số</param>
        /// <param name="commandType">Command type (default: text)</param>
        /// <returns>Collection object</returns>
        IEnumerable<TEntity> GetObject(string sqlCommand = null, object parameters = null,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// Thực hiện thêm mới object vào database
        /// </summary>
        /// <param name="entity">object cần thêm mới</param>
        /// <returns>Số lượng bản ghi thêm được vào database</returns>
        int InsertObject(TEntity entity);

        /// <summary>
        /// Cập nhật object trong database
        /// </summary>
        /// <param name="entity">Object cần cập nhật</param>
        /// <returns>Số object cập nhật thành công</returns>
        int UpdateObject(TEntity entity, string id);

        /// <summary>
        /// Xoá object trong database
        /// </summary>
        /// <returns>Số object xoá thành công</returns>
        int DeleteObject(string[] ids);

        /// <summary>
        /// Kiểm tra id đã tồn tại trong database chưa
        /// </summary>
        /// <param name="id">id cần kiểm tra</param>
        /// <returns>id tìm được hoặc null</returns>
        string CheckEntityIdExist(string id);
    }
}