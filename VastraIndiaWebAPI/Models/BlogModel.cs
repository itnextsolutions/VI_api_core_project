
using Microsoft.AspNetCore.Http;

namespace VastraIndiaWebAPI.Models
{
    public class BlogModel
    {

        public int Blog_Id { get; set; }

        public string Blog_Title { get; set; }

        public string Blog_Content { get; set; }


        public string Blog_Topic { get; set; }


        public string Image_Name { get; set; }
        public int IsActive { get; set; }
        public IFormFile formFile { get; set; }

        public string update_imageName{get;set;}
        //public DateTime Created_Date { get; set; }

        //public DateTime Updated_Date { get; set; }
    }
}
