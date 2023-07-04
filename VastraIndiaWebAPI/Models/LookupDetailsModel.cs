
using Microsoft.AspNetCore.Http;
using System;

namespace VastraIndiaWebAPI.Models
{
    public class LookupDetailsModel
    {
      
        public int Lookup_Details_Id { get; set; }
      
        public int Lookup_Id { get; set; }

        public string Lookup_Name { get; set; }

        public string Description { get; set; }

        public int IsActive { get; set; }
      
        public DateTime Created_Date { get; set; }
      
        public DateTime Updated_Date { get; set; }

        public string ColorName { get; set; }

        public string tipping_img { get; set; }

        public IFormFile formFile { get; set; }

        public string Imagepath { get; set; }

        public string update_imageName { get; set; }
    }
}
