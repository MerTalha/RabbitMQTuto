// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://qgvajewc:zIYAn4-pkcFlUsba0UjT4WRKhdHwle3U@goose.rmq2.cloudamqp.com/qgvajewc");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

//channel.QueueDeclare("hello-queue", true, false, false);

//var randomQueueName = channel.QueueDeclare().QueueName;

//channel.QueueBind(randomQueueName, "logs-fanout", "", null);

channel.BasicQos(0,1,false);

var consumer = new EventingBasicConsumer(channel);

var queueName = channel.QueueDeclare().QueueName;
var routekey = "*.Error.*";

channel.QueueBind(queueName, "logs-topic", routekey);

channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Logları dinleniyor...");

consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    //Thread.Sleep(1500);
    Console.WriteLine("Gelen Mesaj:" + message);

    // File.AppendAllText("log-critical.txt", message+ "\n");

    channel.BasicAck(e.DeliveryTag, false);
};




Console.ReadLine();
