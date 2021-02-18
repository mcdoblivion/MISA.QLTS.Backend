using System.Collections.Generic;
using System.Data;
using MISA.DataLayer.Interfaces;
using MySqlConnector;

namespace MISA.DataLayer.DbContexts
{
    /// <summary>
    /// Kết nối và thao tác database
    /// </summary>
    /// CreatedBy: DMCUONG (08/02/2021)
    public class DbContextV2<TEntity> : IDbContext<TEntity>
    {
        #region DECLARE

        private const string _connectionString =
            "Host = 103.124.92.43;" +
            "Port = 3306;" +
            "Database = DMCuong_MF740_CukCuk_copy;" +
            "UserId = nvmanh;" +
            "password = 12345678;";

        protected readonly IDbConnection _dbConnection;

        #endregion DECLARE

        #region CONSTRUCTOR

        public DbContextV2()
        {
            _dbConnection = new MySqlConnection(_connectionString);
        }

        #endregion CONSTRUCTOR

        #region METHOD

        /// <summary>
        /// Lấy dữ liệu theo nhiều tiêu chí khác nhau
        /// </summary>
        /// <typeparam name="TEntity">Type của object</typeparam>
        /// <param name="sqlCommand">sql command (không truyền: lấy tất cả)</param>
        /// <param name="parameters">Đối tượng chứa thông tin tham số</param>
        /// <param name="commandType">Command type (default: text)</param>
        /// <returns>Data</returns>
        /// CreatedBy: DMCUONG (08/02/2021)
        public IEnumerable<TEntity> GetObject(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text)
        {
            return new List<TEntity>();
        }

        /// <summary>
        /// Thực hiện thêm mới object vào database
        /// </summary>
        /// <param name="entity">object cần thêm mới</param>
        /// <returns>Số lượng bản ghi thêm được vào database</returns>
        /// CreatedBy: DMCUONG (08/02/2021)
        public int InsertObject(TEntity entity)
        {
            return 0;
        }

        /// <summary>
        /// Cập nhật object trong database
        /// </summary>
        /// <param name="entity">Object cần cập nhật</param>
        /// <returns>Số object cập nhật thành công</returns>
        public int UpdateObject(TEntity entity, string id)
        {
            return 0;
        }

        /// <summary>
        /// Xoá object trong database
        /// </summary>
        /// <returns>Số object xoá thành công</returns>
        public int DeleteObject(string[] ids)
        {
            return 0;
        }

        public string CheckEntityIdExist(string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion METHOD
    }
}