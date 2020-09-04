using AcademiaDemo.Application.AppPayment.Input;
using Bogus;

namespace AcademiaDemo.Tests.Comum
{
    internal static class GeneratePaymentFaker
    {
        public static PaymentInput CreatePaymentInputObject(int lenghtCreditCard)
        {
            var input = new Faker<PaymentInput>("pt_BR")
                .RuleFor(c => c.CardNumber, f => f.Random.String(lenghtCreditCard))
                .RuleFor(c => c.SubDivision, f => f.Random.Int(1, 10))
                .RuleFor(c => c.Total, f => f.Random.Decimal())
                .Generate();

            return input;
        }
    }
}
