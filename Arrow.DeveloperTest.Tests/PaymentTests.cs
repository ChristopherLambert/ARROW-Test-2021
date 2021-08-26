using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arrow.DeveloperTest.Tests
{
    [TestFixture]
    public class PaymentTests
    {
        private PaymentService _paymentService;
        private MakePaymentRequest request;
        [SetUp]
        public void SetUp()
        {
            _paymentService = new PaymentService();
            request = new MakePaymentRequest();
        }

        [Test]
        public void IsPaymentTrue()
        {
            request.DebtorAccountNumber = "012345678";
            request.PaymentScheme = PaymentScheme.Bacs;
            var result = _paymentService.MakePayment(request);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void IsPaymenSchemeFalse()
        {
            request.DebtorAccountNumber = "012345678";
            var result = _paymentService.MakePayment(request);
            Assert.IsFalse(result.Success);
        }


        [Test]
        public void IsPaymenAmountValue0()
        {
            request.DebtorAccountNumber = "012345678";
            request.PaymentScheme = PaymentScheme.FasterPayments;
            request.Amount = 0;
            var result = _paymentService.MakePayment(request);
            Assert.IsFalse(result.Success);
        }
    }
}
