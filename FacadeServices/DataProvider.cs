using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using FacadeServices.Interfaces;

namespace FacadeServices
{
    public class DataProvider : IDataProvider
    {
        protected string ConnectionString { get; set; }
        public DataProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public IEnumerable<T> Query<T>(string query, CommandType commandType) where T: class
        {

            SqlConnection conn = new SqlConnection(ConnectionString);
            var items = conn.Query<T>(query, commandType: commandType);
            return items;
        }

        public IEnumerable<T> Query<T>(string query, dynamic param, CommandType commandType) where T : class
        {

            SqlConnection conn = new SqlConnection(ConnectionString);
            var items = SqlMapper.Query<T>(conn, query, param, commandType: commandType);
            return items;
        }

        public int Execute(string query, dynamic param, CommandType commandType)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            return SqlMapper.Execute(conn, sql: query, param: param, commandType: commandType);
        }
    }
}