using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arrow.DeveloperTest.Runner
{
    public class Execute
    {
        private readonly IPaymentService _paymentService;
        public Execute(IPaymentService paymentService)
        {
          
        }

        public void MakePay(MakePaymentRequest request)
        {
            _paymentService.MakePayment(request);
        }
    }
}
