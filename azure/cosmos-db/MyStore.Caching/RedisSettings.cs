using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Caching
{
    public sealed class RedisSettings : IRedisSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface IRedisSettings
    {
        public string ConnectionString { get; }
    }
}
