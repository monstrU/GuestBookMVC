using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using FacadeServices.Interfaces;

namespace FacadeServices
{
    public class DataProvider : IDataProvider
    {
        public IEnumerable<T> Query<T>(string connect, string query, CommandType commandType) where T: class
        {
        
            SqlConnection conn = new SqlConnection(connect);
            var items = conn.Query<T>(query, commandType: commandType);
            return items;
        }

        public IEnumerable<T> Query<T>(string connect, string query,dynamic param, CommandType commandType) where T : class
        {

            SqlConnection conn = new SqlConnection(connect);
            var items = SqlMapper.Query<T>(conn, query, param, commandType: commandType);
            return items;
        }

        public int Execute(string connect, string query, dynamic param, CommandType commandType)
        {
            SqlConnection conn = new SqlConnection(connect);
            return SqlMapper.Execute(conn, sql: query, param: param, commandType: commandType);
        }
    }
}