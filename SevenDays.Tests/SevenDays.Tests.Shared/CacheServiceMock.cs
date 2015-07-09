using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using SevenDays.Model.Entity;

namespace SevenDays.Tests
{
    public class CacheServiceMock : ICacheService
    {
        public Task<T> GetObject<T>(string key) where T: class, new()
        {
            var server = key.Split(':')[0];
            var port = key.Split(':')[1];

            var t = new Server(server, port) as T;

            return Task.FromResult(t);
        }

        public Task<IEnumerable<T>> GetAllObjects<T>()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertObject<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task RemoveObject(string key)
        {
            throw new NotImplementedException();
        }
    }
}
