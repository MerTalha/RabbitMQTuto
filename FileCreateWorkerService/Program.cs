using FileCreateWorkerService;
using FileCreateWorkerService.Services;
using RabbitMQ.Client;




IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services )=>
    {
        IConfiguration configuration = context.Configuration; 


        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();
