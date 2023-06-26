using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Coree.DTO
{
    public class PaymentInfoDTO
    {
        public int IdProject { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvv{ get; set; }
        public string ExpireAt{ get; set; }
        public string FullName{ get; set;}
        public string Amount{ get; set;} 
    }
}
