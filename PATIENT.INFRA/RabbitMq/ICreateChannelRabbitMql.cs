

using RabbitMQ.Client;

namespace PATIENT.INFRA.RabbitMq;

public interface ICreateChannelRabbitMql
{
    IModel GetChannel();
}
