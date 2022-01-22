namespace ERP.Store.API.Entities.Entities
{
    public class Payment : EntityBase
    {
        public bool IsCheck { get; set; }

        public bool IsCard { get; set; }

        public bool IsBankTransfer { get; set; }

        public bool RequiresConfirmation { get; set; }

        public Card Card { get; set; }

        public BankInfo BankInfo { get; set; }
    }
}
