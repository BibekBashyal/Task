using System.ComponentModel.DataAnnotations;

namespace Task_Inficare.ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        public string SenderFirstName { get; set; }

        [Required]
        public string SenderMiddleName { get; set; }

        [Required]
        public string SenderLastName { get; set; }

        [Required]
        public string SenderAddress { get; set; }

        [Required]
        public string SenderCountry { get; set; }

        [Required]
        public string ReceiverFirstName { get; set; }

        [Required]
        public string ReceiverMiddleName { get; set; }

        [Required]
        public string ReceiverLastName { get; set; }

        [Required]
        public string ReceiverAddress { get; set; }

        [Required]
        public string ReceiverCountry { get; set; }

        [Required]
        public string ReceiverBankName { get; set; }

        [Required]
        public string ReceiverAccountNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Transfer amount must be greater than zero.")]
        public decimal TransferAmountMYR { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Exchange rate must be greater than zero.")]
        public decimal ExchangeRate { get; set; }

        public decimal PayoutAmountNPR { get; set; }
    }
}
