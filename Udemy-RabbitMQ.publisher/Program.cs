// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;

Console.WriteLine("Hello, World!");
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://qgvajewc:zIYAn4-pkcFlUsba0UjT4WRKhdHwle3U@goose.rmq2.cloudamqp.com/qgvajewc");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();



