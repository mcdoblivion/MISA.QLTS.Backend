using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MISA.DataLayer.Interfaces;
using MySqlConnector;

namespace MISA.DataLayer.DbContexts
{
    /// <summary>
    /// Kết nối và thao tác database
    /// </summary>
    /// CreatedBy: DMCUONG (05/02/2021)
    public class DbContext<TEntity> : IDbContext<TEntity>
    {
        #region DECLARE

        private const string _connectionString =
            "Host = 47.241.69.179;" +
            "Port = 3306;" +
            "Database = TEST.MISA.QLTS;" +
            "UserId = dev;" +
            "password = 12345678;";

        protected readonly IDbConnection _dbConnection;

        #endregion DECLARE

        #region CONSTRUCTOR

        public DbContext()
        {
            _dbConnection = new MySqlConnection(_connectionString);
        }

        #endregion CONSTRUCTOR

        #region METHOD

        ///// <summary>
        ///// Lấy toàn bộ dữ liệu từ 1 bảng
        ///// </summary>
        ///// <typeparam name="TEntity">Tên bảng</typeparam>
        ///// <returns>Toàn bộ dữ liệu</returns>
        //public IEnumerable<TEntity> GetAll<TEntity>()
        //{
        //    // Thực thi truy vấn lấy dữ liệu
        //    var entities = _dbConnection.Query<TEntity>($"SELECT * FROM {typeof(TEntity).Name}",
        //        commandType: CommandType.Text);
        //    return entities;
        //}

        //public IEnumerable<TEntity> GetAll<TEntity>(string sqlCommand, CommandType commandType = CommandType.Text)
        //{
        //    // Thực thi truy vấn lấy dữ liệu
        //    var entities = _dbConnection.Query<TEntity>(sqlCommand,
        //        commandType: commandType);
        //    return entities;
        //}

        /// <summary>
        /// Lấy dữ liệu theo nhiều tiêu chí khác nhau
        /// </summary>
        /// <typeparam name="TEntity">Type của object</typeparam>
        /// <param name="sqlCommand">sql command (không truyền: lấy tất cả)</param>
        /// <param name="parameters">Đối tượng chứa thông tin tham số</param>
        /// <param name="commandType">Command type (default: text)</param>
        /// <returns>Collection object</returns>
        /// CreatedBy: DMCUONG (07/02/2021)
        public IEnumerable<TEntity> GetObject(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text)
        {
            // Không truyền sqlCommand => lấy tất cả dữ liệu
            if (sqlCommand == null) sqlCommand = $"SELECT * FROM {typeof(TEntity).Name}";

            // Lấy dữ liệu
            var entities = _dbConnection.Query<TEntity>(sqlCommand, param: parameters, commandType: commandType);

            return entities;
        }

        /// <summary>
        /// Thực hiện thêm mới object vào database
        /// </summary>
        /// <param name="entity">object cần thêm mới</param>
        /// <returns>Số lượng bản ghi thêm được vào database</returns>
        /// CreatedBy: DMCUONG (05/02/2021)
        public int InsertObject(TEntity entity)
        {
            var sqlPropName = string.Empty;
            var sqlPropParam = string.Empty;
            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            // Duyệt từng property. Lấy tên và giá trị. Sau đó gán vào câu lệnh sql
            var dynamicParameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(entity);

                // Khi thực hiện thêm mới khoá chính, sinh new guid
                if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(string))
                    if (string.Equals(propName, $"{typeof(TEntity).Name}Id", StringComparison.OrdinalIgnoreCase))
                        propValue = Guid.NewGuid();

                sqlPropName += $", {propName}";
                sqlPropParam += $", @{propName}";

                dynamicParameters.Add($"@{propName}", propValue);
            }

            sqlPropName = sqlPropName.Remove(0, 1);
            sqlPropParam = sqlPropParam.Remove(0, 1);
            var sqlCommand = $"INSERT INTO {typeof(TEntity).Name} ({sqlPropName}) VALUES ({sqlPropParam})";

            var response = _dbConnection.Execute(sqlCommand, param: dynamicParameters, commandType: CommandType.Text);
            return response;
        }

        /// <summary>
        /// Cập nhật object trong database
        /// </summary>
        /// <param name="entity">Object cần cập nhật</param>
        /// <param name="id">Id của object cần cập nhật</param>
        /// <returns>Số object cập nhật thành công</returns>
        public int UpdateObject(TEntity entity, string id)
        {
            var sqlNewValue = string.Empty;

            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            var dynamicParameters = new DynamicParameters();

            // Duyệt từng property. Lấy tên và giá trị. Sau đó gán vào câu lệnh sql
            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(entity);

                if (propName != $"{typeof(TEntity).Name}Id")
                {
                    sqlNewValue = sqlNewValue + ", " + propName + "=" + $"@{propName}";
                    dynamicParameters.Add($"@{propName}", propValue);
                }
            }

            sqlNewValue = sqlNewValue.Remove(0, 2);
            var sqlCommand = $"UPDATE {typeof(TEntity).Name} SET {sqlNewValue} WHERE {typeof(TEntity).Name}Id = '{id}'";
            var response = _dbConnection.Execute(sqlCommand, param: dynamicParameters, commandType: CommandType.Text);
            return response;
        }

        /// <summary>
        /// Xoá object trong database
        /// </summary>
        /// <returns>Số object xoá thành công</returns>
        public int DeleteObject(string[] ids)
        {
            var listId = ids.Select(id => $"'{id}'").ToList();
            var idsQuery = string.Join(",", listId);

            var sql = $"DELETE FROM {typeof(TEntity).Name} WHERE {typeof(TEntity).Name}Id IN ({idsQuery})";
            var response = _dbConnection.Execute(sql, commandType: CommandType.Text);

            return response;
        }

        /// <summary>
        /// Kiểm tra id đã tồn tại trong database chưa
        /// </summary>
        /// <param name="id">id cần kiểm tra</param>
        /// <returns>id tìm được hoặc null</returns>
        public string CheckEntityIdExist(string id)
        {
            var className = typeof(TEntity).Name;
            var sql =
                $"SELECT {className}Id FROM {className} AS T WHERE T.{className}Id = '{id}'";
            var entityIdInDb = _dbConnection.Query<Guid>(sql).FirstOrDefault();
            return entityIdInDb.Equals(Guid.Empty) ? null : entityIdInDb.ToString();
        }

        #endregion METHOD
    }
}