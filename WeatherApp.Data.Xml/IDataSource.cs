using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherApp.Data.Xml
{
    /// <summary>
    /// Abstraction for datasource
    /// </summary>
    public interface IDataSource
    {
        T BuildDataSource<T>();
    }
}
