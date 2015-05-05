using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetObject<T>(string key);
        Task<IEnumerable<T>> GetAllObjects<T>();
        Task<bool> InsertObject<T>(string key, T value);
        Task RemoveObject(string key);
    }
}
