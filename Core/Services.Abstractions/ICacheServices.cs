using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICacheServices
    {
        Task<string?> GetCacheValueAsynk(string key);
        Task SetCacheValueAsynk(string key, object value, TimeSpan duration);
    }
}
