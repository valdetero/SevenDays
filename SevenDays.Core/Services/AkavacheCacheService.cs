using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using SevenDays.Core.Interfaces;

namespace SevenDays.Core.Services
{
    public class AkavacheCacheService: ICacheService
    {
		ILogger _logger;
        public AkavacheCacheService()
        {
			_logger = Ioc.Container.Resolve<ILogger>();
        }

        public async Task RemoveObject(string key)
        {
            try
            {
                await BlobCache.LocalMachine.Invalidate(key);
            }
            catch (Exception exception)
            {
				_logger.LogException(exception);
            }
        }

        public async Task<T> GetObject<T>(string key) where T: class, new()
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<T>(key);
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
            catch (Exception exception)
            {
				_logger.LogException(exception);

				return default(T);
            }
        }

        public async Task<IEnumerable<T>> GetAllObjects<T>()
        {
            try
            {
                return await BlobCache.LocalMachine.GetAllObjects<T>();
            }
            catch (KeyNotFoundException)
            {
                return new List<T>();
            }
            catch (Exception exception)
            {
				_logger.LogException(exception);

				return new List<T>();
            }
        }

        public async Task<bool> InsertObject<T>(string key, T value)
        {
            try
            {
                await BlobCache.LocalMachine.InsertObject(key, value);
            }
            catch (Exception exception)
            {
				_logger.LogException(exception);

                return false;
            }

            return true;
        }
    }
}
