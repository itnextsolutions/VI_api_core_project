
using Microsoft.AspNetCore.Http;

namespace VastraIndiaWebAPI.Models
{
    public class ProductModel
    {

        public int Product_Id { get; set; }

        public int Category_Id { get; set; }

        public int SubCategory_Id { get; set; }

        public string MRP { get; set; }

        public string SizeId { get; set; }

        public string ColorId { get; set; }

        public int[] TipingId { get; set; }

        public int[] TipingWomenId { get; set; }

        public string Product_Title { get; set; }

        public string Product_Description { get; set; }


        public IFormFile[] MenImgFiles { get; set; }

        //public string updateMenImgFiles { get; set; }


        public IFormFile MenFrontImgFile { get; set; }

        public string updateMenFrontImgFile { get; set; }
        public IFormFile FrontImgFile { get; set; }

        public string updateFrontImgFile { get; set; }
        public IFormFile MenSizeChartImgFile { get; set; }
        public string updateMenSizeChartImgFile { get; set; }
        public string Men_f_svgpath { get; set; }

        public string WomenProduct_Description { get; set; }

        public IFormFile[] WomenImgFiles { get; set; }
        public IFormFile WomenFrontImgFile { get; set; }
        public string updateWomenFrontImgFile { get; set; }

        public IFormFile WomenSizeChartImgFile { get; set; }
        public string updateWomenSizeChartImgFile { get; set; }
        public string Women_f_svgpath { get; set; }

    }

}
