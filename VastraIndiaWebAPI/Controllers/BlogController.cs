using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        [Authorize]
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

        // POST api/<CustomerController>
        [Route("api/Blog/InsertBlog")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveBlog([FromForm] BlogModel blog)
        {
            var FileName = "";
            if (blog.formFile != null)
            {
                var Ext = System.IO.Path.GetExtension(blog.formFile.FileName);

                FileName = blog.Blog_Title + "_" + DateTime.Now.ToString("dd-MM-yyyy-hh") + Ext;
            }
            
             string docPath = MyServer.MapPath("Vastra");
            var BlogFolderName = Path.Combine(docPath, "assets", "img", "blog");


            if (!Directory.Exists(BlogFolderName))
            {

                Directory.CreateDirectory(BlogFolderName);
            }

            dt = objblog.InsertBlog(blog.Blog_Title, blog.Blog_Content, blog.Blog_Topic, FileName);

            var SaveImage = saveImage.SaveImagesAsync(blog.formFile, FileName, BlogFolderName);

            return new JsonResult("Added Successfully");
        }

        // POST api/<CustomerController>
        [Route("api/Blog/UpdateBlog")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateBlog([FromForm] BlogModel blog)
        {
            var FileName = "";
            if (blog.formFile!=null)
            {
                var Ext = System.IO.Path.GetExtension(blog.formFile.FileName);

                FileName = blog.Blog_Title + "_" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm") + Ext;
            }
            



            string docPath = MyServer.MapPath("Vastra");
            var BlogFolderName = Path.Combine(docPath, "assets", "img", "blog");

            if (!Directory.Exists(BlogFolderName))
            {
               Directory.CreateDirectory(BlogFolderName);
            }

            if (FileName != null && FileName!="")
            {
                dt = objblog.UpdateBlog(blog.Blog_Id, blog.Blog_Title, blog.Blog_Topic, blog.Blog_Content, FileName);
                var SaveImage = saveImage.SaveImagesAsync(blog.formFile, FileName, BlogFolderName);
            }

            if (blog.update_imageName != null && blog.update_imageName != "")
            {
                dt = objblog.UpdateBlog(blog.Blog_Id, blog.Blog_Title, blog.Blog_Topic, blog.Blog_Content, blog.update_imageName);
                var SaveImage = saveImage.SaveImagesAsync(blog.formFile, blog.update_imageName, BlogFolderName);
            }


            return new JsonResult("Updated Successfully");
        }



        // DELETE api/<BlogController>/5


        [HttpPost]
        [Authorize]
        [Route("api/Blog/Delete")]
        public JsonResult Delete([FromBody] int Blog_Id)
        {
            dt = objblog.DeleteBlog(Blog_Id);
            return new JsonResult("Deleted Successfully");

        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
