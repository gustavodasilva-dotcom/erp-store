using System;

namespace ERP.Store.API.Entities.Tables
{
    public class UserInfoData
    {
        public int User_InfoID { get; set; }

        public string Username { get; set; }

        public byte[] Password { get; set; }

        public int Deleted { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
