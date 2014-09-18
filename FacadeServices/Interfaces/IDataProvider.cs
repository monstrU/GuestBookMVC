using System.Collections.Generic;
using System.Data;

namespace FacadeServices.Interfaces
{
    public interface IDataProvider 
    {
        IEnumerable<T> Query<T>(string connect, string query, CommandType commandType) where T: class;
        int Execute(string connect, string query, dynamic param, CommandType commandType);
        IEnumerable<T> Query<T>(string connect, string query,dynamic param, CommandType commandType) where T : class;
    }
}