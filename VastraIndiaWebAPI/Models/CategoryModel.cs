
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace VastraIndiaWebAPI.Models
{
    public class CategoryModel
    {
        public int Category_Id { get; set; }

        [StringLength(50)]
        public string Category_Name { get; set; }
        public int? IsActive { get; set; }

        [StringLength(50)]
        public string Category_Description { get; set; }

        public string Category_Photo { get; set; }

        public string Imagepath { get; set; }

        public IFormFile formFile { get; set; }
    }
}
