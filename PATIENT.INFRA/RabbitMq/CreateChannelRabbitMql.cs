using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace PATIENT.INFRA.RabbitMq;

public class CreateChannelRabbitMql : ICreateChannelRabbitMql
{
    private IConnection _connection;
    private IModel _channel;
    private string RABBIT_HOST;
    private string RABBIT_PORT;
    private string RABBIT_USERNAME;
    private string RABBIT_PASSWORD;
    private readonly ILogger<CreateChannelRabbitMql> _logger;
    private readonly IConfiguration _configuration;

    public CreateChannelRabbitMql(
    ILogger<CreateChannelRabbitMql> logger,
        IConfiguration configuration)
    {
        RABBIT_HOST = configuration.GetSection("RabbitMqSettings")["HOST"] ?? string.Empty;
        RABBIT_PORT = configuration.GetSection("RabbitMqSettings")["PORT"] ?? string.Empty;
        RABBIT_USERNAME = configuration.GetSection("RabbitMqSettings")["USERNAME"] ?? string.Empty;
        RABBIT_PASSWORD = configuration.GetSection("RabbitMqSettings")["PASSWORD"] ?? string.Empty;
        _logger = logger;
        _configuration = configuration;
        CreateConnection();
    }

    private void CreateConnection()
    {
        try
        {
            var rabbitMQConfig = _configuration.GetSection("RabbitMQ");
            if (_channel is not null) return;

            _connection = new ConnectionFactory
            {
                AmqpUriSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                Endpoint = new AmqpTcpEndpoint(RABBIT_HOST, int.Parse(RABBIT_PORT)),
                UserName = RABBIT_USERNAME,
                Password = RABBIT_PASSWORD
            }.CreateConnection();

            _channel = _connection.CreateModel();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while creating RabbitMq Connection.");
            _logger.LogError(ex.Message, ex);
        }
    }

    public IModel GetChannel()
        => _channel;
}
