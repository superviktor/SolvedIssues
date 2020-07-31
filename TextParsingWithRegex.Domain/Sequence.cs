using System;

namespace TextParsingWithRegex.Domain
{
    public class Sequence
    {
        public string PaymentId { get; set; }
        public string KID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDateTime { get; set; }
        public string BankAccountNumberPaymentReceivedFrom { get; set; }
        public string BankAccountNumberPaymentReceivedTo { get; set; }
    }
}
