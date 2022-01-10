namespace ERP.Store.API.Entities.Entities
{
    public class BankInfo
    {
        public bool IsMobileTransfer { get; set; }

        public bool IsEletronicBankTransfer { get; set; }

        public string Number { get; set; }

        public string Agency { get; set; }

        public string BankName { get; set; }
    }
}
