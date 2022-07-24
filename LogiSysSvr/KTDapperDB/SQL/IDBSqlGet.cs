using System;
using System.Collections.Generic;
using System.Text;

namespace KTDapperDB.SQL
{
    public interface IDBSqlGet<T>
    {
        string GetSql(T data);
    }
}
