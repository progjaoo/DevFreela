using System.Text;
using System.Text.Json;
using DevFreela.Coree.DTO;
using DevFreela.Coree.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Payments
{

    public class PaymentService : IPaymentService
    { 
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";

        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            //passa objeto PaymentDTO para JSON
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            //passa o json para bytes
            var paymentInfoByte = Encoding.UTF8.GetBytes(paymentInfoJson);
            //passa a fila e os bytes do json serializado
            _messageBusService.Publish(QUEUE_NAME, paymentInfoByte);
        }
    }
}
