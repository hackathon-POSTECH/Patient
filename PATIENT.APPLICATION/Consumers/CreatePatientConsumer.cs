using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PATIENT.APPLICATION.Patient.CreatePatient;
using PATIENT.INFRA.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PATIENT.APPLICATION.Consumers;

public class CreatePatientConsumer : BackgroundService
{
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public CreatePatientConsumer(ICreateChannelRabbitMql createChannelRabbitMql, IServiceProvider serviceProvider)
    {
        _channel = createChannelRabbitMql.GetChannel();
        _serviceProvider = serviceProvider;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        CreateConsumer();
    }

    private void CreateConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (s, e) => await Consumer(s, e);
        _channel.BasicQos(0, 20, false);
        _channel.BasicConsume(EventConstants.CREATE_USER_QUEUE, false, consumer);
    }

    private async Task Consumer(object sender, BasicDeliverEventArgs e)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var request = JsonConvert.DeserializeObject<CreatePatientModel>(message);
                if (!string.IsNullOrEmpty(request.Crm)) throw new Exception();

                var command = new CreatePatientCommand(request.UserId, request.Name, request.Email, request.Cpf);

                await mediator.Send(command);
                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception expection)
            {
                _channel.BasicNack(e.DeliveryTag, false, true);
            }
        }
    }

    private class CreatePatientModel()
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Crm { get; set; }
    }
}
