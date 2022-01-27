namespace ERP.Store.Desktop.Entities.JSON.Request
{
    public class PaymentRequest
    {
        public bool IsCheck { get; set; }

        public bool IsCard { get; set; }

        public bool IsBankTransfer { get; set; }

        public CardRequest Card { get; set; }

        public BankInfoRequest BankInfo { get; set; }
    }
}