// See https://aka.ms/new-console-template for more information


using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://qgvajewc:zIYAn4-pkcFlUsba0UjT4WRKhdHwle3U@goose.rmq2.cloudamqp.com/qgvajewc");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

//channel.QueueDeclare("hello-queue", true, false, false);

channel.ExchangeDeclare("logs-topic", durable: true, type: ExchangeType.Topic);


Enumerable.Range(1, 100).ToList().ForEach(x =>
{
    Random rnd = new Random();
    LogNames log1 = (LogNames)rnd.Next(1, 5);
    LogNames log2 = (LogNames)rnd.Next(1, 5);
    LogNames log3 = (LogNames)rnd.Next(1, 5);

    var routeKey = $"{log1}.{log2}.{log3}";
    string message = $"Log: {log1}.{log2}.{log3}";
    var messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish("logs-topic", routeKey, null, messageBody);

    Console.WriteLine($"Gönderildi: {message}");
});

Console.ReadLine();


public enum LogNames
{
    Critical = 1,
    Error = 2,
    Warning = 3,
    Info = 4
}

