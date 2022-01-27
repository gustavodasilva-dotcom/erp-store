namespace ERP.Store.Desktop.Entities.JSON.Request
{
    public class CardRequest
    {
        public bool IsCredit { get; set; }

        public string NameOnCard { get; set; }

        public string CardNumber { get; set; }

        public int YearExpiryDate { get; set; }

        public int MonthExpiryDate { get; set; }

        public int SecurityCode { get; set; }
    }
}