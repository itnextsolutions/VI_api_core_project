using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VastraIndiaDAL;
using VastraIndiaWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VastraIndiaWebAPI.Controllers
{


    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductController : ControllerBase
    {

       


        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index1()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string path = "";
            string path1 = Path.Combine(webRootPath, "CSS");
            path = Path.Combine(contentRootPath, "wwwroot", "CSS");
            return new JsonResult(path);
        }

        DataTable dt = new DataTable();
        ProductDAL objProductDAL = new ProductDAL();
        SqlHelper objsqlHelper = new SqlHelper();
        SaveImageDAL saveImage = new SaveImageDAL();

        [HttpGet]
        [Authorize]
        [Route("api/Product/GetProdutCatDropDown")]
        public JsonResult GetProdutCatDropDown()
        {
            dt = objProductDAL.GetProdutCatDropDown();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        //Products start
        // GET: api/<ProductController>
        [HttpGet]
        
        [Route("api/Product/GetProducts")]
        public JsonResult GetProduts()
        {
            dt = objProductDAL.GetProduct();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/GetProductsByid")]
        [HttpGet("{id}")]
        
        public IActionResult GetProdutsByid(int id)
        {
            dt = objProductDAL.GetProductById(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/DeleteProduct")]
        [HttpPost]
        [Authorize]
        // DELETE api/<ProductController>/5
        public JsonResult Delete([FromBody]int Product_Id)
        {

            dt = objProductDAL.DeleteProduct(Product_Id);
            return new JsonResult("Deleted Successfully");
        }





        // POST api/<ProductController>
        [Route("api/Product/InsertProduct")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveProduct([FromForm] ProductModel product)
        {
            var FrontPhoto = "";
            var MenFrontPhoto = "";
            var MenSizeChartName = "";
            var WomenFrontPhoto = "";
            var WomenSizeChart = "";

            if (product.FrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.FrontImgFile.FileName);

                FrontPhoto = product.Product_Title + "Front" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.MenFrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.MenFrontImgFile.FileName);

                MenFrontPhoto = product.Product_Title + "MenFront" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            var mrp ="";
            decimal MRP = 0;
            if (product.MRP!=null)
            {
                mrp=product.MRP;
                MRP = decimal.Parse(mrp);
            }
           


            if (product.MenSizeChartImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.MenSizeChartImgFile.FileName);

                MenSizeChartName = product.Product_Title + "MenSizeChart" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.WomenFrontImgFile.FileName);

                WomenFrontPhoto = product.Product_Title + "WomenFront" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }



            if (product.WomenSizeChartImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.WomenSizeChartImgFile.FileName);

                WomenSizeChart = product.Product_Title + "WomenSizeChart" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            string Description = "";
            if (product.Product_Description != null)
            {
                Description = product.Product_Description;
            }

            string WomenDescription = "";
            if (product.WomenProduct_Description != null)
            {
                WomenDescription = product.WomenProduct_Description;
            }

            string men_f_svgpath = "";
            if (product.Men_f_svgpath != null)
            {
                men_f_svgpath = product.Men_f_svgpath;
            }

            string women_f_svgpath = "";
            if (product.Women_f_svgpath != null)
            {
                women_f_svgpath = product.Women_f_svgpath;
            }

            dt = objProductDAL.GetProductCategoryById(product.Category_Id);
            string categoryname = ((string)dt.Rows[0]["Category_Name"]).ToLower();

            string CategoryName = categoryname.Replace(" ", "-");




            string docPath = MyServer.MapPath("Vastra");
            var ProductFolderbyCategoryName = Path.Combine(docPath, "assets", "img", CategoryName);


            if (!Directory.Exists(ProductFolderbyCategoryName))
            {
                Directory.CreateDirectory(ProductFolderbyCategoryName);
            }

            string xmlcolor = "";
            if (product.ColorId != null)
            {
                string ColorId = product.ColorId;
                List<int> ColorIds = ColorId.Split(',').Select(int.Parse).ToList();
                int[] ColorIdsInArray = ColorIds.ToArray();
                xmlcolor = objsqlHelper.ListStrAryToXML(ColorIdsInArray, "colors", "colorcode", "colorid");
            }

            string xmlsize = "";
            if (product.SizeId != null)
            {
                string SizeId = product.SizeId;
                List<int> SizeIdS = SizeId.Split(',').Select(int.Parse).ToList();
                int[] SizeIdSInArray = SizeIdS.ToArray();
                xmlsize = objsqlHelper.ListStrAryToXML(SizeIdSInArray, "size", "sizecode", "sizeid");
            }


            string SizeChartPath = MyServer.MapPath("Vastra");
            var ProductFolderbySizeChart = Path.Combine(SizeChartPath, "assets", "img", "size_chart");

            if (!Directory.Exists(ProductFolderbySizeChart))
            {
                Directory.CreateDirectory(ProductFolderbySizeChart);
            }

            
            dt = objProductDAL.InsertProduct(product.Category_Id, product.SubCategory_Id, product.Product_Title, Description, FrontPhoto, MenFrontPhoto, /*MenSidephotoName, MenBackphotoName,*/ MenSizeChartName, WomenDescription, WomenFrontPhoto, /*WomenSidePhoto, WomenBackPhoto,*/ WomenSizeChart, xmlcolor, xmlsize, men_f_svgpath, women_f_svgpath,MRP);

            var SaveImage = saveImage.SaveProductImageAsync(product.FrontImgFile, product.MenFrontImgFile, /*product.MenSideImgFile, product.MenBackImgFile,*/ /*product.MenSizeChartImgFile,*/FrontPhoto, MenFrontPhoto, /*MenSidephotoName, MenBackphotoName,*/ /*MenSizeChartName,*/ product.WomenFrontImgFile, /*product.WomenSideImgFile, product.WomenBackImgFile,*/ /*product.WomenSizeChartImgFile,*/ WomenFrontPhoto, /*WomenSidePhoto, WomenBackPhoto,*/ /*WomenSizeChart,*/ ProductFolderbyCategoryName);

            var SaveSizaChartImage = saveImage.SaveSizeChartImageAsync(product.MenSizeChartImgFile, product.WomenSizeChartImgFile, MenSizeChartName, WomenSizeChart, ProductFolderbySizeChart);

            return new JsonResult("Added Successfully");

            
        }

        [Route("api/Product/InsertMultiProduct")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveMultiProduct([FromForm] ProductModel product)
        {

            var MenFrontPhoto = "";
            var MenSizeChartName = "";
            var WomenFrontPhoto = "";
            var WomenSizeChart = "";



            var mrp = "";
            decimal MRP = 0;
            if (product.MRP != null)
            {
                mrp = product.MRP;
                MRP = decimal.Parse(mrp);
            }
            

            var fileMenName = new List<string>();
             if (product.MenImgFiles != null)
            {
                foreach (var file in product.MenImgFiles)
                {

                    var Ext = System.IO.Path.GetExtension(file.FileName);

                    var name = System.IO.Path.GetFileNameWithoutExtension(file.FileName);


                    var MenPhoto = name + DateTime.Now.ToString("-dd-MM-yyyy-HH") + Ext;

                    fileMenName.Add(MenPhoto);
                }

            }

           

            string[] ImageMenNameInArray = fileMenName.ToArray();

            var fileWomenName = new List<string>();

            if (product.WomenImgFiles != null)
            {
                foreach (var file in product.WomenImgFiles)
                {

                    var Ext = System.IO.Path.GetExtension(file.FileName);

                    var name = System.IO.Path.GetFileNameWithoutExtension(file.FileName);


                    var WomenPhoto = name + DateTime.Now.ToString("-dd-MM-yyyy-HH") + Ext;

                    fileWomenName.Add(WomenPhoto);
                }

            }

      

            string[] ImageWomenNameInArray = fileWomenName.ToArray();


            string xmlmenWithid = "";
            if (product.TipingId != null)
            {
                string[] mentipping = product.TipingId.Select(x => x.ToString()).ToArray();
                var menarray = (mentipping.Zip(ImageMenNameInArray, (Id, menFileName) => (Id, menFileName)));
                xmlmenWithid = objsqlHelper.ListStrAryToXMLMen(menarray, "tippingDetail", "tippingid");
            }



            string xmlwomenWithid = "";
            if (product.TipingWomenId != null)
            {
                string[] womentipping = product.TipingWomenId.Select(x => x.ToString()).ToArray();

                var womenarray = womentipping.Zip(ImageWomenNameInArray, (Id, womenFileName) => (Id, womenFileName));

                xmlwomenWithid = objsqlHelper.ListStrAryToXMLWomen(womenarray, "tippingDetail", "tippingid");
            }

            string Description = "";
            if (product.Product_Description != null)
            {
                Description = product.Product_Description;
            }


            if (product.MenFrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.MenFrontImgFile.FileName);

                MenFrontPhoto = product.Product_Title + "MenFront" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.MenSizeChartImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.MenSizeChartImgFile.FileName);

                MenSizeChartName = product.Product_Title + "MenSizeChart" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.WomenFrontImgFile.FileName);

                WomenFrontPhoto = product.Product_Title + "WomenFront" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.WomenSizeChartImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.WomenSizeChartImgFile.FileName);

                WomenSizeChart = product.Product_Title + "WomenSizeChart" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }


            dt = objProductDAL.GetProductCategoryById(product.Category_Id);
            string categoryname = ((string)dt.Rows[0]["Category_Name"]).ToLower();

            string CategoryName = categoryname.Replace(" ", "-");

            string docPath = MyServer.MapPath("Vastra");
            var ProductFolderbyCategoryName = Path.Combine(docPath, "assets", "img", CategoryName);


            if (!Directory.Exists(ProductFolderbyCategoryName))
            {
               Directory.CreateDirectory(ProductFolderbyCategoryName);
            }

            string xmlsize = "";

            if (product.SizeId != null)
            {
                string SizeId = product.SizeId;
                List<int> SizeIdS = SizeId.Split(',').Select(int.Parse).ToList();
                int[] SizeIdSInArray = SizeIdS.ToArray();

                xmlsize = objsqlHelper.ListStrAryToXML(SizeIdSInArray, "size", "sizecode", "sizeid");
            }

          
            string SizeChartPath = MyServer.MapPath("Vastra");
            var ProductFolderbySizeChart = Path.Combine(SizeChartPath, "assets", "img", "size_chart");

            if (!Directory.Exists(ProductFolderbySizeChart))
            {
                Directory.CreateDirectory(ProductFolderbySizeChart);
            }


            dt = objProductDAL.InsertMultiProduct(product.Category_Id, product.SubCategory_Id, product.Product_Title, Description, xmlmenWithid, xmlwomenWithid, xmlsize, MenFrontPhoto, MenSizeChartName, WomenFrontPhoto, WomenSizeChart,MRP);

            var SaveImage = saveImage.SaveMultiProductImagesAsync(product.MenImgFiles, product.WomenImgFiles, product.MenFrontImgFile, /*product.MenSizeChartImgFile,*/ product.WomenFrontImgFile, /*product.WomenSizeChartImgFile,*/ MenFrontPhoto, /*MenSizeChartName,*/ WomenFrontPhoto,/* WomenSizeChart,*/ ProductFolderbyCategoryName);

            var SaveSizaChartImage = saveImage.SaveSizeChartImageAsync(product.MenSizeChartImgFile, product.WomenSizeChartImgFile, MenSizeChartName, WomenSizeChart, ProductFolderbySizeChart);

            return new JsonResult("Added Successfully");


        }


        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateProduct")]
        //  [HttpPut("{id}")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateProduct([FromForm] ProductModel product)
        {
            var FrontPhoto = "";
            var MenFrontPhoto = "";
            var MenSizeChartName = "";
            var WomenFrontPhoto = "";
            var WomenSizeChart = "";

            var mrp = "";
            decimal MRP = 0;
            if (product.MRP != null)
            {
                mrp = product.MRP;
                MRP = decimal.Parse(mrp);
            }
            

            if (product.FrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.FrontImgFile.FileName);

                FrontPhoto = product.Product_Title + "Front" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.MenFrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.MenFrontImgFile.FileName);

                MenFrontPhoto = product.Product_Title + "_" + "Front" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;

            }



            if (product.MenSizeChartImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.MenSizeChartImgFile.FileName);

                MenSizeChartName = product.Product_Title + "_" + "MenSizeChart" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.WomenFrontImgFile.FileName);

                WomenFrontPhoto = product.Product_Title + "_" + "WomenFront" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }



            if (product.WomenSizeChartImgFile != null)
            {
                var Ext = System.IO.Path.GetExtension(product.WomenSizeChartImgFile.FileName);

                WomenSizeChart = product.Product_Title + "_" + "WomenSizeChart" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }


            dt = objProductDAL.GetProductCategoryById(product.Category_Id);
            string categoryname = ((string)dt.Rows[0]["Category_Name"]).ToLower();

            string CategoryName = categoryname.Replace(" ", "-");

            string docPath = MyServer.MapPath("Vastra");
            var ProductFolderbyCategoryName = Path.Combine(docPath, "assets", "img", CategoryName);

            if (!Directory.Exists(ProductFolderbyCategoryName))
            {
               Directory.CreateDirectory(ProductFolderbyCategoryName);
            }

            string xmlcolor = "";
            if (product.ColorId != null)
            {
                string ColorId = product.ColorId;
                List<int> ColorIds = ColorId.Split(',').Select(int.Parse).ToList();
                int[] ColorIdsInArray = ColorIds.ToArray();
                xmlcolor = objsqlHelper.ListStrAryToXML(ColorIdsInArray, "colors", "colorcode", "colorid");
            }

            string xmlsize = "";
            if (product.SizeId != null)
            {
                string SizeId = product.SizeId;
                List<int> SizeIdS = SizeId.Split(',').Select(int.Parse).ToList();
                int[] SizeIdSInArray = SizeIdS.ToArray();
                xmlsize = objsqlHelper.ListStrAryToXML(SizeIdSInArray, "size", "sizecode", "sizeid");
            }

            string Description = "";
            if (product.Product_Description != null)
            {
                Description = product.Product_Description;
            }

            string WomenDescription = "";
            if (product.WomenProduct_Description != null)
            {
                WomenDescription = product.WomenProduct_Description;
            }

            string men_f_svgpath = "";
            if (product.Men_f_svgpath != null)
            {
                men_f_svgpath = product.Men_f_svgpath;
            }

            string women_f_svgpath = "";
            if (product.Women_f_svgpath != null)
            {
                women_f_svgpath = product.Women_f_svgpath;
            }
            string SizeChartPath = MyServer.MapPath("Vastra");
            var ProductFolderbySizeChart = Path.Combine(SizeChartPath, "assets", "img", "size_chart");

            if (!Directory.Exists(ProductFolderbySizeChart))
            {
               Directory.CreateDirectory(ProductFolderbySizeChart);
            }

            dt = objProductDAL.UpdateProduct(product.Product_Id, product.Category_Id, product.SubCategory_Id, product.Product_Title, Description, FrontPhoto, MenFrontPhoto, /*MenSidephotoName, MenBackphotoName,*/ MenSizeChartName, WomenDescription, WomenFrontPhoto, /*WomenSidePhoto, WomenBackPhoto,*/ WomenSizeChart, xmlcolor, xmlsize, men_f_svgpath, women_f_svgpath,MRP);
            var SaveImage = saveImage.SaveProductImageAsync(product.FrontImgFile, product.MenFrontImgFile,/* product.MenSideImgFile, product.MenBackImgFile,*/ /*product.MenSizeChartImgFile,*/FrontPhoto, MenFrontPhoto, /*MenSidephotoName, MenBackphotoName,*/ /*MenSizeChartName,*/ product.WomenFrontImgFile, /*product.WomenSideImgFile, product.WomenBackImgFile,*/ /*product.WomenSizeChartImgFile,*/ WomenFrontPhoto, /*WomenSidePhoto, WomenBackPhoto,*/ /*WomenSizeChart,*/ ProductFolderbyCategoryName);

            var SaveSizaChartImage = saveImage.SaveSizeChartImageAsync(product.MenSizeChartImgFile, product.WomenSizeChartImgFile, MenSizeChartName, WomenSizeChart, ProductFolderbySizeChart);

            return new JsonResult("Updated Successfully");

        }

        //Products end


        //Category start
        [Route("api/Product/GetCategory")]
        [Authorize]
        public JsonResult GetCategory()
        {
            dt = objProductDAL.GetProductCategory();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }


        [Route("api/Product/GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            dt = objProductDAL.GetProductCategoryById(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/DeleteCategory")]
        [HttpPost]
        [Authorize]
        // DELETE api/<ProductController>/5
        public JsonResult DeleteCategory([FromBody]  int category_id)
        {

            dt = objProductDAL.DeleteCategory(category_id);
            return new JsonResult("Deleted Successfully");

        }


        [Route("api/Product/InsertCategory")]
        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult> SaveProductcategory([FromForm] CategoryModel category)
        {
            var FileName = "";
            if (category.formFile != null)
            {
                var Ext = System.IO.Path.GetExtension(category.formFile.FileName);

                FileName = category.Category_Name + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;
            }

            var CategoryFolderName = "";
            string IsBrand = "";
            if (category.IsBrand != null)
            {
                IsBrand = category.IsBrand;
                string docPath = MyServer.MapPath("Vastra");
                CategoryFolderName = Path.Combine(docPath, "assets", "img", "brand");
               
            }
            else
            {
                IsBrand = "0";
                string docPath = MyServer.MapPath("Vastra");
                CategoryFolderName = Path.Combine(docPath, "assets", "img", "category");
                
            }

            if (!Directory.Exists(CategoryFolderName))
            {
               Directory.CreateDirectory(CategoryFolderName);
            }

            int brand = Convert.ToInt32(IsBrand);

            dt = objProductDAL.InsertCategory(category.Category_Name, FileName, category.Category_Description, brand);
            var SaveImage = saveImage.SaveImagesAsync(category.formFile, FileName, CategoryFolderName);
            return new JsonResult("Added Successfully");

        }


        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateCategory")]
        // [HttpPut("{id}")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateProductcategory([FromForm] CategoryModel category)
        {
            var FileName = "";
            if (category.formFile != null)
            {
                var Ext = System.IO.Path.GetExtension(category.formFile.FileName);

                FileName = category.Category_Name + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;
            }

            var CategoryFolderName = "";
            string IsBrand = "";
            if (category.IsBrand != null)
            {
                IsBrand = category.IsBrand;
                string docPath = MyServer.MapPath("Vastra");
                CategoryFolderName = Path.Combine(docPath, "assets", "img", "brand");
               
            }
            else
            {
                IsBrand = "0";
                string docPath = MyServer.MapPath("Vastra");
                CategoryFolderName = Path.Combine(docPath, "assets", "img", "category");
               
            }

            int brand = Convert.ToInt32(IsBrand);

            if (!Directory.Exists(CategoryFolderName))
            {
                Directory.CreateDirectory(CategoryFolderName);
            }

            dt = objProductDAL.UpdateCategory(category.Category_Id, category.Category_Name, FileName, category.Category_Description, brand);
            var SaveImage = saveImage.SaveImagesAsync(category.formFile, FileName, CategoryFolderName);
            return new JsonResult("Updated Successfully");
        }
        //Category end

        //ProductColor start
        [Route("api/Product/GetProductColor")]
        public JsonResult GetProductColor()
        {
            dt = objProductDAL.GetProductColor();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/GetProductColorById")]
        public JsonResult GetProductColorByid(int id)
        {
            dt = objProductDAL.GetProductColorById(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/DeleteProdctColor")]
        [HttpPost("{id}")]
        [Authorize]
        // DELETE api/<ProductController>/5
        public void DeleteProdctColor(int id)
        {

            dt = objProductDAL.DeleteProdctColor(id);

        }


        [Route("api/Product/InsertProdctColor")]
        [HttpPost]
        public IActionResult Post([FromBody] ProductColor color)
        {
            dt = objProductDAL.InsertProdctColor(color.Product_Id, color.Color_Code);
            return new JsonResult("Added Successfully");
        }

        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateProdctColor")]
        // [HttpPut("{id}")]
        [HttpPost]
        public IActionResult Put([FromBody] ProductColor color)
        {
            dt = objProductDAL.UpdateProdctColor(color.Product_Color_Id, color.Product_Id, color.Color_Code);
            return new JsonResult("Updated Successfully");
        }
        //ProductColor end


        //ProductSize start
        [Route("api/Product/GetProductSize")]
        [Authorize]
        public JsonResult GetProductSize()
        {
            dt = objProductDAL.ProductSize();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }


        [Route("api/Product/GetProductSizeById")]
        public JsonResult GetProductSizeById(int id)
        {
            dt = objProductDAL.GetProductSizeById(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/DeleteProductSize")]
        [HttpPost("{id}")]
        [Authorize]
        // DELETE api/<ProductController>/5
        public void DeleteProductSize(int id)
        {

            dt = objProductDAL.DeleteProductSize(id);

        }


        [Route("api/Product/InsertProdctColor")]
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ProductSize size)
        {
            dt = objProductDAL.InsertProductSize(size.Product_Id, size.Size);
            return new JsonResult("Added Successfully");
        }


        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateProductSize")]
        //  [HttpPut("{id}")]
        [HttpPost]
        public IActionResult Put([FromBody] ProductSize size)
        {
            dt = objProductDAL.UpdateProductSize(size.ProductSizeId, size.Product_Id, size.Size);
            return new JsonResult("Updated Successfully");
        }


        //SubCategory start


        //SubCategory start
        [Authorize]
        [Route("api/Product/GetSubProductCategory")]
        public JsonResult GetSubProductCategory()
        {
            dt = objProductDAL.GetSubCategory();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }


        [Route("api/Product/GetSubCategory")]
        public JsonResult GetSubCategory()
        {
            dt = objProductDAL.GetSubCategory();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }


        [Route("api/Product/GetSubCategoryById")]
        public IActionResult GetSubCategoryById(int id)
        {
            dt = objProductDAL.GetSubProductCategoryById(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }


        [Route("api/Product/InsertSubCategory")]
        [HttpPost]
        [Authorize]
        public IActionResult InsertSubCategory([FromForm] SubProductCategoryModel subcategorymodel)
        {
            dt = objProductDAL.InsertSubCategory(subcategorymodel.Category_Id, subcategorymodel.SubCategory);
            return new JsonResult("Added Successfully");
        }

        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateSubCategory")]
        // [HttpPut("{id}")]
        [HttpPost]
        [Authorize]
        public IActionResult Put([FromForm] SubProductCategoryModel subcategorymodel)
        {
            dt = objProductDAL.UpdateSubCategory(subcategorymodel.SubCategory_Id, subcategorymodel.Category_Id, subcategorymodel.SubCategory);
            return new JsonResult("Updated Successfully");
        }

        [Route("api/Product/DeleteSubCategory")]
        [HttpPost]
        [Authorize]
        // DELETE api/<ProductController>/5
        public JsonResult DeleteSubCategory([FromBody]int Id)
        {

            dt = objProductDAL.DeleteSubCategory(Id);
            return new JsonResult("Deleted Successfully");
        }
        //SubCategory end


        //Get Sub Cat Dropdown by Cat id
        [Route("api/Product/GetSubCatByCatid")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetSubCatByCatid(int id)
        {
            dt = objProductDAL.GetSubCatByCatid(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }



        [Route("api/Product/GetColorCodeListByProductId")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetColorCodeListByProductId(int id)
        {
            dt = objProductDAL.GetColorCodeListByProductId(id);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        //GetProductCategoryPagination


        [Route("api/Product/GetProductCategoryPagination")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetProductCategoryPagination(int pageNo, int pageSize)
        {
            //dt = objProductDAL.GetColorCodeListByProductId(id);
            dt = objProductDAL.GetProductCategoryPagination(pageNo, pageSize);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }



        [Route("api/Product/GetProductCategoryCount")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetProductCategoryCount()
        {
           dt = objProductDAL.GetProductCategoryCount();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }


        //SubCategoryPagination

        [Route("api/Product/SubCategoryPagination")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult SubCategoryPagination(int pageNo, int pageSize)
        {
            dt = objProductDAL.SubCategoryPagination(pageNo, pageSize);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/SubCategoryCount")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult SubCategoryCount()
        {
           dt = objProductDAL.SubCategoryCount();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }
        //SubCategoryPagination End



        //ProductPagination
        [Route("api/Product/GetProductPagination")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetProductPagination(int pageNo, int pageSize)
        {
            dt = objProductDAL.GetProductPagination(pageNo, pageSize);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        [Route("api/Product/GetProductCount")]
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetProductCount()
        {
            dt = objProductDAL.GetProductCount();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in dt.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return new JsonResult(parentRow);
        }

        //ProductPagination End




    }
}
