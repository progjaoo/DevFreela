using System.Text;
using System.Text.Json;
using DevFreela.Coree.DTO;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Payments
{

    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentsBaseUrl;

        public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        }
        
        public async Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            //passa objeto PaymentDTO para JSON
            var url = $"{_paymentsBaseUrl}/api/payments";
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            //CRIA UM CONTEUDO 
            var paymentInfoContent = new StringContent(
                paymentInfoJson,
                Encoding.UTF8,
                "application/json");

            //USA O CONTEUDO PARA FAZER A REQUISIÇÃO HTTP
            var httpClient = _httpClientFactory.CreateClient("Payments");

            //REALIZA REQUISIÇÃO HTTP;
            var response =  await httpClient.PostAsync(url, paymentInfoContent);

            //Recebendo uma resposta e retorna o tipo BOOL.
            return response.IsSuccessStatusCode;       
        }
    }
}
