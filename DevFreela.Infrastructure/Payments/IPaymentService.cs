using DevFreela.Coree.DTO;

namespace DevFreela.Infrastructure.Payments
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
