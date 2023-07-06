using DevFreela.Coree.DTO;

namespace DevFreela.Infrastructure.Payments
{
    public interface IPaymentService
    {
        void ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
