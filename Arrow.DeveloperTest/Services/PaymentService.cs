using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Types;
using System.Configuration;

namespace Arrow.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountDataStoreGetData = new AccountDataStore();
            Account account = accountDataStoreGetData.GetAccount(request.DebtorAccountNumber);
            
            var result = new MakePaymentResult();
            result.Success = CanRealizePayment(account, request);

            if (result.Success)
            {
                account.Balance -= request.Amount;
                var accountDataStoreUpdateData = new AccountDataStore();
                accountDataStoreUpdateData.UpdateAccount(account);
            }

            return result;
        }

        private bool CanRealizePayment(Account account, MakePaymentRequest request)
        {
            if (account == null) return false;

            else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs)
                || !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments)
                || !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                return false;

            switch (request.PaymentScheme)
            {
                case PaymentScheme.FasterPayments:
                    if (account.Balance < request.Amount)
                        return false; break;

                case PaymentScheme.Chaps:
                    if (account.Status != AccountStatus.Live)
                        return false; break;
            }

            return true;
        }
    }
}
