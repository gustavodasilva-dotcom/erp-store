using System;

namespace ERP.Store.API.Entities.Tables
{
    public class ImageData
    {
        public int ImageID { get; set; }

        public string Base64 { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
