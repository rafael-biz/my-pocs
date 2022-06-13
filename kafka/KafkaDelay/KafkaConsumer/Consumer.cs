using Confluent.Kafka;

const string topic = "test-topic";

var config = new ConsumerConfig
{
    GroupId = "test-consumer-group",
    BootstrapServers = "127.0.0.1:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest,
};

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => {
    e.Cancel = true;
    cts.Cancel();
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

consumer.Subscribe(topic);

Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} - Subscribed to: [{topic}]");

try
{
    while (!cts.IsCancellationRequested)
    {
        try
        {
            var result = consumer.Consume(cts.Token);

            DateTime target = result.Message.Timestamp.UtcDateTime + TimeSpan.FromSeconds(5);

            DateTime now = DateTime.UtcNow;

            if (target > now)
            {
                TimeSpan sleep = target - now;

                consumer.Pause(consumer.Assignment);

                await Task.Delay(sleep);

                consumer.Resume(consumer.Assignment);

                consumer.Assign(result.TopicPartitionOffset);

                continue;
            }

            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} - Consumed [{topic}]: '{result.Message.Value}'");
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Error [{topic}]: {e.Error.Reason}");
        }
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
}