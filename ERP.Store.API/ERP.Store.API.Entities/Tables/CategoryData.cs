using System;

namespace ERP.Store.API.Entities.Tables
{
    public class CategoryData
    {
        public int CategoryID { get; set; }

        public string Description { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
