
using Microsoft.AspNetCore.Http;

namespace VastraIndiaWebAPI.Models
{
    public class ProductModel
    {
        public int Product_Id { get; set; }

        public int Category_Id { get; set; }

        public int SubCategory_Id { get; set; }

        public string SizeId { get; set; }

        public string ColorId { get; set; }

        public string Product_Title { get; set; }

        public string Product_Description { get; set; }

        //public string Image_Name { get; set; }

        public IFormFile MenFrontImgFile { get; set; }

        public IFormFile MenSideImgFile { get; set; }
        public IFormFile MenBackImgFile { get; set; }

        public IFormFile MenSizeChartImgFile { get; set; }

        public string WomenProduct_Description { get; set; }
        public IFormFile WomenFrontImgFile { get; set; }

        public IFormFile WomenBackImgFile { get; set; }
        public IFormFile WomenSideImgFile { get; set; }

        public IFormFile WomenSizeChartImgFile { get; set; }

    }

}
