using Confluent.Kafka;

const string topic = "test-topic";

while (true)
{
    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} - Please enter a message:");

    string line = Console.ReadLine();

    var config = new ProducerConfig { BootstrapServers = "127.0.0.1:9092" };

    using var p = new ProducerBuilder<Null, string>(config).Build();

    var message = new Message<Null, string> { Value = line };

    var dr = await p.ProduceAsync(topic, message);

    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} - Delivered to [{topic}]: '{dr.Value}'");
}