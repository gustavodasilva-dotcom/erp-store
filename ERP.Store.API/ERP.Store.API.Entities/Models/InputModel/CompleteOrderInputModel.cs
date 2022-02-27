namespace ERP.Store.API.Entities.Models.InputModel
{
    public class CompleteOrderInputModel
    {
        public int OrderID { get; set; }

        public bool CompleteOrder { get; set; }

        public bool CancelOrder { get; set; }
    }
}
