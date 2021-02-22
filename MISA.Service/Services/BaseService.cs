using MISA.Common.Models;
using MISA.Common.Properties;
using MISA.DataLayer;
using MISA.DataLayer.DbContexts;
using MISA.DataLayer.Interfaces;
using MISA.Service.Interfaces;

namespace MISA.Service.Services
{
    /// <summary>
    /// Xử lý nghiệp vụ chung
    /// </summary>
    /// <typeparam name="TEntity">Đối tượng xử lý</typeparam>
    /// CreatedBy: DMCUONG (07/02/2021)
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        #region Declare

        private readonly IDbContext<TEntity> _dbContext;

        #endregion Declare

        #region Constructor

        public BaseService(IDbContext<TEntity> iDbContext)
        {
            _dbContext = iDbContext;
        }

        #endregion Constructor

        #region Method

        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>ServiceResult</returns>
        public virtual ServiceResult Get()
        {
            var serviceResult = new ServiceResult()
            {
                Data = _dbContext.GetObject()
            };

            return serviceResult;
        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>ServiceResult</returns>
        public ServiceResult Get(string id)
        {
            var serviceResult = new ServiceResult()
            {
                Data = _dbContext.GetObject(sqlCommand: $"SELECT * FROM {typeof(TEntity).Name} WHERE {typeof(TEntity).Name}Id='{id}'")
            };

            return serviceResult;
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>ServiceResult</returns>
        public virtual ServiceResult Insert(TEntity entity)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();
            var isValid = IsInsertDataValid(entity, errorMsg);

            if (isValid)
            {
                // Validate thành công thì thực hiện thêm mới:
                var response = _dbContext.InsertObject(entity);
                serviceResult.Data = response;
                serviceResult.Success = true;
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
            }
            return serviceResult;
        }

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật</param>
        /// <param name="id">id bản ghi</param>
        /// <returns>ServiceResult</returns>
        public virtual ServiceResult Update(TEntity entity, string id)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();
            var isValid = IsUpdateDataValid(id, entity, errorMsg);

            if (isValid)
            {
                // Validate thành công thì thực hiện cập nhật:
                var response = _dbContext.UpdateObject(entity, id);
                serviceResult.Data = response;
                serviceResult.Success = true;
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
            }
            return serviceResult;
        }

        /// <summary>
        /// Xoá 1 bản ghi
        /// </summary>
        /// <param name="ids">mảng id bản ghi</param>
        /// <returns>ServiceResult</returns>
        public virtual ServiceResult Delete(string[] ids)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();

            var isExist = true;
            foreach (var id in ids)
            {
                if (!IsDataExist(id, errorMsg)) isExist = false;
            }

            if (isExist)
            {
                var response = _dbContext.DeleteObject(ids);
                serviceResult.Success = true;
                serviceResult.Data = response;
                return serviceResult;
            }

            serviceResult.Data = errorMsg;
            serviceResult.Success = false;
            return serviceResult;
        }

        /// <summary>
        /// Validate dữ liệu thêm mới
        /// </summary>
        /// <param name="entity">Thực thể cần validate</param>
        /// <param name="errorMsg">Thông báo lỗi</param>
        /// <returns>true - hợp lệ; false - không hợp lệ</returns>
        protected virtual bool IsInsertDataValid(TEntity entity, ErrorMsg errorMsg = null)
        {
            return true;
        }

        /// <summary>
        /// Validate dữ liệu cập nhật
        /// </summary>
        /// <param name="id">Id của thực thể cần validate</param>
        /// <param name="entity">Thực thể cần validate</param>
        /// <param name="errorMsg">Thông báo lỗi</param>
        /// <returns>true - hợp lệ; false - không hợp lệ</returns>
        protected virtual bool IsUpdateDataValid(string id, TEntity entity, ErrorMsg errorMsg = null)
        {
            return true;
        }

        /// <summary>
        /// Kiểm tra dữ liệu có tồn tại trong database không
        /// </summary>
        /// <param name="id">id của entity</param>
        /// <param name="errorMsg">Thông báo lỗi</param>
        /// <returns>true - tồn tại, false - không tồn tại</returns>
        protected virtual bool IsDataExist(string id, ErrorMsg errorMsg = null)
        {
            var idInDb = _dbContext.CheckEntityIdExist(id);
            if (errorMsg == null) errorMsg = new ErrorMsg();
            if (idInDb != null) return true;
            errorMsg.UserMsg.Add(Resources.ErrorService_IdNotExist + $": {id}");
            return false;
        }

        #endregion Method
    }
}