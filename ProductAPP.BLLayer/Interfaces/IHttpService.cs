using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPP.BLLayer.Interfaces
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task<T> Post<T>(string uri, object value);
        Task<T> Delete<T>(string uri);
        Task<T> Put<T>(string uri, object value);
    }
}
