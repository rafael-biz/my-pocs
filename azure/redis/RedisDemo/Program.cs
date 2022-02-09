using System;
using System.Configuration;
using StackExchange.Redis;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RedisDemo
{
    class Program
    {
        private static readonly string CacheConnection = ConfigurationManager.AppSettings["CacheConnection"];

        static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(CacheConnection);

        static async Task Main(string[] args)
        {
            var db = redis.GetDatabase();
            var pong = await db.PingAsync();
            Console.WriteLine($"Ping: {pong}\n");

            string id = Guid.NewGuid().ToString();

            Employee employee1 = new Employee()
            {
                Id = id,
                Name = "Severino",
                Age = 55
            };

            string json = JsonConvert.SerializeObject(employee1);

            bool result = db.StringSet(id, json, TimeSpan.FromSeconds(3));

            Console.WriteLine($"Set result: {result}\n");

            RedisValue redisValue = db.StringGet(id);

            if (redisValue.HasValue)
            {
                Employee employee2 = JsonConvert.DeserializeObject<Employee>(json);
                Console.WriteLine("Get result:");
                Console.WriteLine("\tEmployee.Name : " + employee2.Name);
                Console.WriteLine("\tEmployee.Id   : " + employee2.Id);
                Console.WriteLine("\tEmployee.Age  : " + employee2.Age + "\n");
            }
            else
            {
                Console.WriteLine("Value not found!");
            }

            Console.WriteLine($"Waiting...\n");

            await Task.Delay(TimeSpan.FromSeconds(5));

            redisValue = db.StringGet(id);

            if (redisValue.HasValue)
            {
                Console.WriteLine("Value didn't expired!");
            }
            else
            {
                Console.WriteLine("Value has expired as expected!");
            }
        }

        class Employee
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

    }
}
