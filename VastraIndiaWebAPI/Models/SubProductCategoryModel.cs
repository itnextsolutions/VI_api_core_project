using System;

namespace VastraIndiaWebAPI.Models
{
    public class SubProductCategoryModel
    {
        public int SubCategory_Id { get; set; }
        public int Category_Id { get; set; }
        public string SubCategory { get; set; }
        //public string Sub_Cat_Photo { get; set; }

        public int IsBrand { get; set; }
        public int IsActive { get; set; }

        public DateTime Created_Date { get; set; }

        public DateTime Updated_Date { get; set; }
    }
}
