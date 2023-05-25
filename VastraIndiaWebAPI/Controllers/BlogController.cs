using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using VastraIndiaDAL;
using VastraIndiaWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VastraIndiaWebAPI.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class BlogController : ControllerBase
    {
        DataTable dt = new DataTable();
        BlogDAL objblog = new BlogDAL();
        SaveImageDAL saveImage = new SaveImageDAL();

        // GET: api/<BlogController>
        [HttpGet]
        [Route("api/Blog/GetBlog")]
        public JsonResult GetBlog()
        {
            dt = objblog.GetBlog();
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

        [HttpGet]
        [Route("api/Blog/GetAllBlog")]
        public JsonResult GetAllBlog()
        {
            dt = objblog.GetAllBlog();
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

        // GET api/<BlogController>/5
        [Route("api/Blog/GetBlogById")]
        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            dt = objblog.GetBlogById(id);

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
        //// POST api/<BlogController>
        //[Route("api/Blog/InsertBlog")]
        //[HttpPost]
        //public IActionResult Post([FromBody] BlogModel blog)
        //{
        //    if (blog.Blog_Title != null)
        //    {
        //        dt = objblog.InsertBlog(blog.Blog_Title, blog.Blog_Content, blog.Blog_Topic, blog.Image_Name);
        //        return new JsonResult("Added Successfully");
        //    }
        //    else
        //    {
        //        return new JsonResult("Please Enter All Details");
        //    }
        //}

        // POST api/<CustomerController>
        [Route("api/Blog/InsertBlog")]
        [HttpPost]
        public async Task<ActionResult> SaveBlog([FromForm] BlogModel blog)
        {
            var FileName = "";
            if (blog.formFile != null)
            {
                var Ext = System.IO.Path.GetExtension(blog.formFile.FileName);

                FileName = blog.Blog_Title + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;
            }
            
            //var BlogFolderName = Path.Combine("C:", "Projects", "VasraIndia_local", "Vastra", "src", "assets", "img", "blog");

            string docPath = MyServer.MapPath("Vastra");
            var BlogFolderName = Path.Combine(docPath, "assets", "img", "blog");


            if (!Directory.Exists(BlogFolderName))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(BlogFolderName);
            }

            dt = objblog.InsertBlog(blog.Blog_Title, blog.Blog_Content, blog.Blog_Topic, FileName);

            var SaveImage = saveImage.SaveImagesAsync(blog.formFile, FileName, BlogFolderName);

            return new JsonResult("Added Successfully");
        }

        // POST api/<CustomerController>
        [Route("api/Blog/UpdateBlog")]
        [HttpPost]
        public async Task<ActionResult> UpdateBlog([FromForm] BlogModel blog)
        {
            var FileName = "";
            if (blog.formFile!=null)
            {
                var Ext = System.IO.Path.GetExtension(blog.formFile.FileName);

                FileName = blog.Blog_Title + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;
            }
            

            //var BlogFolderName = Path.Combine("C:", "Projects", "VasraIndia_local", "Vastra","src","assets","img","blog");

            string docPath = MyServer.MapPath("Vastra");
            var BlogFolderName = Path.Combine(docPath, "assets", "img", "blog");

            if (!Directory.Exists(BlogFolderName))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(BlogFolderName);
            }

            dt = objblog.UpdateBlog(blog.Blog_Id, blog.Blog_Title, blog.Blog_Topic, blog.Blog_Content, FileName);
         
            var SaveImage = saveImage.SaveImagesAsync(blog.formFile, FileName, BlogFolderName);

            return new JsonResult("Added Successfully");
        }


        //// PUT api/<BlogController>/5
        //[Route("api/Blog/UpdateBlog")]
        ////  [HttpPut("{id}")]
        //[HttpPut]
        //public IActionResult Put([FromBody] BlogModel blog)
        //{
        //    if (blog != null)
        //    {
        //        if (blog.Blog_Id != 0)
        //        {
        //            dt = objblog.UpdateBlog(blog.Blog_Id, blog.Blog_Title, blog.Blog_Topic, blog.Blog_Content, blog.Image_Name);
        //            return new JsonResult("Updated Successfully");
        //        }
        //    }
        //    return new JsonResult("Data is not valid");

        //}

        // DELETE api/<BlogController>/5
        [Route("api/Blog/Delete")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            dt = objblog.DeleteBlog(id);
            return new JsonResult("Deleted Successfully");

        }

        [HttpGet]
        [Route("api/Blog/GetAllBlogPagination")]
        public JsonResult GetAllBlogPagination(int pageNo, int pageSize)
        {
            dt = objblog.GetAllBlogPagination(pageNo,pageSize);
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

        [HttpGet]
        [Route("api/Blog/GetAllBlogCount")]
        public JsonResult GetAllBlogCount()
        {
            dt = objblog.GetAllBlogCount();
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
    }
}
