using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using SevenDays.Core.Interfaces;

namespace SevenDays.Core.Services
{
    public class AkavacheCacheService: ICacheService
    {
        public AkavacheCacheService()
        {
            
        }
 
        public async Task RemoveObject(string key)
        {
            await BlobCache.LocalMachine.Invalidate(key);
        }
 
        public async Task<T> GetObject<T>(string key)
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<T>(key);
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }
 
        public async Task InsertObject<T>(string key, T value)
        {
            await BlobCache.LocalMachine.InsertObject(key, value);
        }
    }
}
