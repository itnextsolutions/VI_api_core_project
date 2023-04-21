using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data;
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

        DataTable dt = new DataTable();
        ProductDAL objProductDAL = new ProductDAL();
        SqlHelper objsqlHelper = new SqlHelper();
        SaveImageDAL saveImage = new SaveImageDAL();

        [HttpGet]
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
        [HttpDelete("{id}")]
        // DELETE api/<ProductController>/5
        public JsonResult Delete(int id)
        {

            dt = objProductDAL.DeleteProduct(id);
            return new JsonResult("Deleted Successfully");
        }



        // POST api/<ProductController>
        [Route("api/Product/InsertProduct")]
        [HttpPost]
        public async Task<ActionResult> SaveProduct([FromForm] ProductModel product)
        {
            

            var MenSidephotoName = "";
            var MenBackphotoName = "";
            var MenSizeChartName = "";  
            var WomenFrontPhoto = "";
            var WomenSidePhoto = "";
            var WomenBackPhoto = "";
            var WomenSizeChart = "";

            var Ext = System.IO.Path.GetExtension(product.MenFrontImgFile.FileName);

            var MenFrontPhoto = product.Product_Title + "MenFront" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;

            if (product.MenSideImgFile != null)
            {
                MenSidephotoName = product.Product_Title + "MenSide" +DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.MenBackImgFile != null)
            {
                MenBackphotoName = product.Product_Title + "MenBack" + Ext;
            }

            if (product.MenSizeChartImgFile != null)
            {
                MenSizeChartName = product.Product_Title + "MenSizeChart" + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                WomenFrontPhoto = product.Product_Title + "WomenFront" + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                WomenSidePhoto = product.Product_Title + "WomenSide" + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                WomenBackPhoto = product.Product_Title + "WomenBack" + Ext;
            }

            if (product.WomenSizeChartImgFile != null)
            {
                WomenSizeChart = product.Product_Title + "WomenSizeChart" + Ext;
            }


            dt = objProductDAL.GetProductCategoryById(product.Category_Id);
            string CategoryName = (string)dt.Rows[0]["Category_Name"];

            //  var ProductFolderbyCategoryName = Path.Combine("C:", "Projects", "Alpesh_VastraPro", "Vastra", "src", "assets", "img", CategoryName);

            var ProductFolderbyCategoryName = Path.Combine("C:", "Projects", "VasraIndia_local", "Vastra", "src", "assets", "img", CategoryName);

            if (!Directory.Exists(ProductFolderbyCategoryName))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(ProductFolderbyCategoryName);
            }


            string ColorId = product.ColorId;
            List<int> ColorIds = ColorId.Split(',').Select(int.Parse).ToList();
            int[] ColorIdsInArray = ColorIds.ToArray();

            string SizeId = product.SizeId;
            List<int> SizeIdS = SizeId.Split(',').Select(int.Parse).ToList();
            int[] SizeIdSInArray = SizeIdS.ToArray();

            string xmlcolor = objsqlHelper.ListStrAryToXML(ColorIdsInArray, "colors", "colorcode", "colorid");
            string xmlsize = objsqlHelper.ListStrAryToXML(SizeIdSInArray, "size", "sizecode", "sizeid");
            dt = objProductDAL.InsertProduct(product.Category_Id, product.SubCategory_Id, product.Product_Title, product.Product_Description, MenFrontPhoto, MenSidephotoName, MenBackphotoName, MenSizeChartName,  product.WomenProduct_Description, WomenFrontPhoto, WomenSidePhoto, WomenBackPhoto, WomenSizeChart, xmlcolor, xmlsize);

            // var SaveImage = saveImage.SaveProductImagesAsync(product.formFile, product.file1, product.file2, FileName, SidephotoName, BackphotoName, ProductFolderbyCategoryName);
            var SaveImage = saveImage.SaveProductImagesAsync(product.MenFrontImgFile, product.MenSideImgFile, product.MenBackImgFile, product.MenSizeChartImgFile, MenFrontPhoto, MenSidephotoName, MenBackphotoName, MenSizeChartName, product.WomenFrontImgFile, product.WomenSideImgFile, product.WomenBackImgFile, product.WomenSizeChartImgFile, WomenFrontPhoto, WomenSidePhoto, WomenBackPhoto, WomenSizeChart, ProductFolderbyCategoryName);
            return new JsonResult("Added Successfully");


        }
        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateProduct")]
        //  [HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromForm] ProductModel product)
        {
            //var SidephotoName = "";
            //var BackphotoName = "";
            //var Ext = System.IO.Path.GetExtension(product.Men_formFile.FileName);

            //var FileName = product.Product_Title + "_" + "Front" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;


            //if (product.Men_file1 != null)
            //{
            //    SidephotoName = product.Product_Title + "_" + "Side" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            //}

            //if (product.Men_file2 != null)
            //{
            //    BackphotoName = product.Product_Title + "_" + "Back" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            //}

            //dt = objProductDAL.GetProductCategoryById(product.Category_Id);
            //string CategoryName = (string)dt.Rows[0]["Category_Name"];



            //var ProductFolderbyCategoryName = Path.Combine("C:", "Vishal", "Projects", "VastraIndiaProject", "Vastra", "src", "assets", "img", CategoryName);
            //if (!Directory.Exists(ProductFolderbyCategoryName))
            //{
            //    //If Directory (Folder) does not exists. Create it.
            //    Directory.CreateDirectory(ProductFolderbyCategoryName);
            //}

            //string ColorId = product.ColorId;
            //List<int> ColorIds = ColorId.Split(',').Select(int.Parse).ToList();
            //int[] ColorIdsInArray = ColorIds.ToArray();

            //string SizeId = product.SizeId;
            //List<int> SizeIdS = SizeId.Split(',').Select(int.Parse).ToList();
            //int[] SizeIdSInArray = SizeIdS.ToArray();

            //string xmlcolor = objsqlHelper.ListStrAryToXML(ColorIdsInArray, "colors", "colorcode", "colorid");
            //string xmlsize = objsqlHelper.ListStrAryToXML(SizeIdSInArray, "size", "sizecode", "sizeid");

            //dt = objProductDAL.UpdateProduct(product.Product_Id, (int)product.Category_Id, 4, product.Product_Title, product.Product_Description, FileName, SidephotoName, BackphotoName, xmlcolor, xmlsize);
            //var SaveImage = saveImage.SaveProductImagesAsync(product.Men_formFile, product.Men_file1, product.Men_file2, FileName, SidephotoName, BackphotoName, ProductFolderbyCategoryName);
            //return new JsonResult("Updated Successfully");

            var MenSidephotoName = "";
            var MenBackphotoName = "";
            var MenSizeChartName = "";
            //var WomenDescription = "";
            var WomenFrontPhoto = "";
            var WomenSidePhoto = "";
            var WomenBackPhoto = "";
            var WomenSizeChart = "";
            var Ext = System.IO.Path.GetExtension(product.MenFrontImgFile.FileName);

            var MenFrontPhoto = product.Product_Title + "_" + "Front" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;


            if (product.MenSideImgFile != null)
            {
                MenSidephotoName = product.Product_Title + "_" + "Side" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.MenBackImgFile != null)
            {
                MenBackphotoName = product.Product_Title + "_" + "Back" + "_" + DateTime.Now.ToString("dd-MM-yyyy-HHmm") + Ext;
            }

            if (product.MenSizeChartImgFile != null)
            {
                MenSizeChartName = product.Product_Title + "MenSizeChart" + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                WomenFrontPhoto = product.Product_Title + "WomenFront" + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                WomenSidePhoto = product.Product_Title + "WomenSide" + Ext;
            }

            if (product.WomenFrontImgFile != null)
            {
                WomenBackPhoto = product.Product_Title + "WomenBack" + Ext;
            }

            if (product.WomenSizeChartImgFile != null)
            {
                WomenSizeChart = product.Product_Title + "WomenSizeChart" + Ext;
            }


            dt = objProductDAL.GetProductCategoryById(product.Category_Id);
            string CategoryName = (string)dt.Rows[0]["Category_Name"];

            var ProductFolderbyCategoryName = Path.Combine("C:", "Projects", "VasraIndia_local", "Vastra", "src", "assets", "img", CategoryName);

            // var ProductFolderbyCategoryName = Path.Combine("C:", "Projects", "Alpesh_VastraPro", "Vastra", "src", "assets", "img", CategoryName);
            if (!Directory.Exists(ProductFolderbyCategoryName))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(ProductFolderbyCategoryName);
            }

            string ColorId = product.ColorId;
            List<int> ColorIds = ColorId.Split(',').Select(int.Parse).ToList();
            int[] ColorIdsInArray = ColorIds.ToArray();

            string SizeId = product.SizeId;
            List<int> SizeIdS = SizeId.Split(',').Select(int.Parse).ToList();
            int[] SizeIdSInArray = SizeIdS.ToArray();

            string xmlcolor = objsqlHelper.ListStrAryToXML(ColorIdsInArray, "colors", "colorcode", "colorid");
            string xmlsize = objsqlHelper.ListStrAryToXML(SizeIdSInArray, "size", "sizecode", "sizeid");

            dt = objProductDAL.UpdateProduct(product.Product_Id,product.Category_Id, product.SubCategory_Id,product.Product_Title, product.Product_Description, MenFrontPhoto, MenSidephotoName, MenBackphotoName, MenSizeChartName,product.WomenProduct_Description, WomenFrontPhoto, WomenSidePhoto, WomenBackPhoto, WomenSizeChart, xmlcolor, xmlsize);
            var SaveImage = saveImage.SaveProductImagesAsync(product.MenFrontImgFile, product.MenSideImgFile, product.MenBackImgFile, product.MenSizeChartImgFile, MenFrontPhoto, MenSidephotoName, MenBackphotoName, MenSizeChartName, product.WomenFrontImgFile, product.WomenSideImgFile, product.WomenBackImgFile, product.WomenSizeChartImgFile, WomenFrontPhoto, WomenSidePhoto, WomenBackPhoto, WomenSizeChart, ProductFolderbyCategoryName);
            return new JsonResult("Updated Successfully");

        }

        //Products end


        //Category start
        [Route("api/Product/GetCategory")]
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
        [HttpDelete("{id}")]
        // DELETE api/<ProductController>/5
        public JsonResult DeleteCategory(int id)
        {

            dt = objProductDAL.DeleteCategory(id);
            return new JsonResult("Deleted Successfully");

        }


        [Route("api/Product/InsertCategory")]
        [HttpPost("")]
        public async Task<ActionResult> SaveProductcategory([FromForm] CategoryModel category)
        {
            var Ext = System.IO.Path.GetExtension(category.formFile.FileName);

            var FileName = category.Category_Name + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;

            var CategoryFolderName = Path.Combine("C:", "Projects", "VasraIndia_local", "Vastra", "src", "assets", "img","category");

            if (!Directory.Exists(CategoryFolderName))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(CategoryFolderName);
            }

            dt = objProductDAL.InsertCategory(category.Category_Name, FileName, category.Category_Description);
            var SaveImage = saveImage.SaveImagesAsync(category.formFile, FileName, CategoryFolderName);
            return new JsonResult("Added Successfully");

        }


        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateCategory")]
        // [HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult> UpdateProductcategory([FromForm] CategoryModel category)
        {

            var Ext = System.IO.Path.GetExtension(category.formFile.FileName);

            var FileName = category.Category_Name + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;

            var CategoryFolderName = Path.Combine("C:", "Projects", "VasraIndia_local", "Vastra", "src", "assets", "img", "category");

            if (!Directory.Exists(CategoryFolderName))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(CategoryFolderName);
            }
            dt = objProductDAL.UpdateCategory(category.Category_Id, category.Category_Name, FileName, category.Category_Description);
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
        [HttpDelete("{id}")]
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
        [HttpPut]
        public IActionResult Put([FromBody] ProductColor color)
        {
            dt = objProductDAL.UpdateProdctColor(color.Product_Color_Id, color.Product_Id, color.Color_Code);
            return new JsonResult("Updated Successfully");
        }
        //ProductColor end


        //ProductSize start
        [Route("api/Product/GetProductSize")]
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
        [HttpDelete("{id}")]
        // DELETE api/<ProductController>/5
        public void DeleteProductSize(int id)
        {

            dt = objProductDAL.DeleteProductSize(id);

        }


        [Route("api/Product/InsertProdctColor")]
        [HttpPost]
        public IActionResult Post([FromBody] ProductSize size)
        {
            dt = objProductDAL.InsertProductSize(size.Product_Id, size.Size);
            return new JsonResult("Added Successfully");
        }


        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateProductSize")]
        //  [HttpPut("{id}")]
        [HttpPut]
        public IActionResult Put([FromBody] ProductSize size)
        {
            dt = objProductDAL.UpdateProductSize(size.ProductSizeId, size.Product_Id, size.Size);
            return new JsonResult("Updated Successfully");
        }


        //SubCategory start


        //SubCategory start

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
        public IActionResult Post([FromBody] SubProductCategoryModel subcategory)
        {
            dt = objProductDAL.InsertSubCategory(subcategory.Category_Id, subcategory.Sub_Cat_Name);
            return new JsonResult("Added Successfully");
        }

        // PUT api/<ProductController>/5
        [Route("api/Product/UpdateSubCategory")]
        // [HttpPut("{id}")]
        [HttpPut]
        public IActionResult Put([FromBody] SubProductCategoryModel subcategory)
        {
            dt = objProductDAL.UpdateSubCategory(subcategory.Id, subcategory.Category_Id, subcategory.Sub_Cat_Name);
            return new JsonResult("Updated Successfully");
        }

        [Route("api/Product/DeleteSubCategory")]
        [HttpDelete("{id}")]
        // DELETE api/<ProductController>/5
        public JsonResult DeleteSubCategory(int id)
        {

            dt = objProductDAL.DeleteSubCategory(id);
            return new JsonResult("Deleted Successfully");
        }
        //SubCategory end


        //Get Sub Cat Dropdown by Cat id
        [Route("api/Product/GetSubCatByCatid")]
        [HttpGet("{id}")]
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
        public IActionResult GetProductCategoryCount()
        {
            //dt = objProductDAL.GetColorCodeListByProductId(id);
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
        public IActionResult SubCategoryPagination(int pageNo, int pageSize)
        {
            //dt = objProductDAL.GetColorCodeListByProductId(id);
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
        public IActionResult SubCategoryCount()
        {
            //dt = objProductDAL.GetColorCodeListByProductId(id);
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
