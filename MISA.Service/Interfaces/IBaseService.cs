using MISA.Common.Models;

namespace MISA.Service.Interfaces
{
    /// <summary>
    /// Interface ngắt sự phụ thuộc của API vào Service
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// CreatedBy: DMCUONG (07/02/2021)
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>ServiceResult</returns>
        ServiceResult Get();

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>ServiceResult</returns>
        ServiceResult Insert(TEntity entity);

        /// <summary>
        /// Xoá 1 bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>ServiceResult</returns>
        ServiceResult Delete(string[] ids);

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật</param>
        /// <param name="id">id bản ghi</param>
        /// <returns>ServiceResult</returns>
        ServiceResult Update(TEntity entity, string id);
    }
}