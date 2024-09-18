using System.Transactions;

namespace Task_Inficare.ViewModels
{
    public class TransactionReportViewModel
    {
        public List<Task_Inficare.Models.Transaction> Transactions { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
