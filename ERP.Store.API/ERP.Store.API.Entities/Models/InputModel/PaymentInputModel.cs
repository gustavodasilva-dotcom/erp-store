namespace ERP.Store.API.Entities.Models.InputModel
{
    public class PaymentInputModel
    {
        public bool IsCheck { get; set; }

        public bool IsCard { get; set; }

        public bool IsBankTransfer { get; set; }

        public CardInputModel Card { get; set; }

        public BankInfoInputModel BankInfo { get; set; }
    }
}
