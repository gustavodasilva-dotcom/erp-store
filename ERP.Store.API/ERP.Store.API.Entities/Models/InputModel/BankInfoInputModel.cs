namespace ERP.Store.API.Entities.Models.InputModel
{
    public class BankInfoInputModel
    {
        public bool IsMobileTransfer { get; set; }

        public bool IsEletronicBankTransfer { get; set; }

        public string Number { get; set; }

        public string Agency { get; set; }

        public string BankName { get; set; }
    }
}
