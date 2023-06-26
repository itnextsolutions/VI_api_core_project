using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Collections.Generic;
using System.Data;
using VastraIndiaDAL;
using MimeKit;
using MimeKit.Text;
using VastraIndiaWebAPI.Models;
using System.Net.Mail;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VastraIndiaWebAPI.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]   
    public class HomeController : ControllerBase
    {
        DataTable dt = new DataTable();
        HomeDAL objHomeDAL = new HomeDAL();
        BlogDAL objblog = new BlogDAL();


        /* GET FAQ */
        [HttpGet]       
        [Route("api/Home/GetFaq")]
        public JsonResult GetFaq()
        {
            dt = objHomeDAL.GetFaq();
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

        [HttpPost]
        [Route("api/Home/SendEmail")]
        public JsonResult SendEmail([FromBody] Home body)
        {
            if(body != null)
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(body.email));
                email.Cc.Add(MailboxAddress.Parse(body.email));
                email.To.Add(MailboxAddress.Parse("yogigole1824@gmail.com"));
                if(body.subject ==null)
                {
                    string subject;
                    subject = "Enquiry";
                    body.subject = subject;
                    email.Subject = body.subject;
                }
                else
                {
                    email.Subject = body.subject;
                }

                //newbody = body.email;
                //newbody += body.subject;
                //newbody += body.message;

                string name = body.name;
                string fromail = body.email;
                string message = body.message;
                message += "<br>" + "<b>Name:</b> " + name + "<br>" + " <b>From:</b> " + fromail ;


                email.Body = new TextPart(TextFormat.Html) { Text = message };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 465);
                smtp.Authenticate("yogigole1824@gmail.com", "hrwjmbhogwcwhrbo");
                smtp.Send(email);
                smtp.Disconnect(true);
                return new JsonResult("Message Send Successfully");
            }
            return new JsonResult("Please Enter Required Fields");


        }

        [Route("api/Home/GetTippingCodeListByProductId")]
        [HttpGet("{id}")]
       
        public IActionResult GetTippingCodeListByProductId(int id)
        {
            dt = objHomeDAL.GetTippingCodeListByProductId(id);
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


        [Route("api/Home/GetTippingWomenByProductId")]
        [HttpGet("{id}")]
       
        public IActionResult GetTippingWomEnListByProductId(int id)
        {
            dt = objHomeDAL.GetTippingWomenListByProductId(id);
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

        /* Menu Binding */
        [HttpGet]

        [Route("api/Home/GetMenuList")]
        [Authorize]
        public JsonResult GetMenuList()
        {
            dt = objHomeDAL.GetMenuList();
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


        //Notification
        [HttpGet]
       
        [Route("api/Home/GeNotification")]
        public JsonResult GetNotification()
        {
            dt = objHomeDAL.GetNotification();
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

        [HttpGet]
       
        [Route("api/Home/GetCustomerReview")]
        public JsonResult GetCustomerReview()
        {
            dt = objHomeDAL.GetCustomerReview();
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

        //Search Result
        [Route("api/Home/GetSearchProduct")]
        [HttpGet("{search}")]
       
        public IActionResult GetSearchProduct(string search)
        {
            dt = objHomeDAL.GetSearchProduct(search);
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

        //Product Menubar start
        [Route("api/Home/GetProductMenu")]
        public JsonResult GetProductMenu()
        {
            dt = objHomeDAL.GetProductMenu();
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

        
        [Route("api/Home/GetSimillarProducts")]
        [HttpGet("{id}")]
       
        public IActionResult GetSimillarProducts(int id)
        {
            dt = objHomeDAL.GetSimillarProducts(id);
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

        [Route("api/Home/GetProductsBySubCategory")]
        [HttpGet("{id}/{subCatId}")]
       
        public IActionResult GetProdutsBySubCategory(int id, int subCatId)
        {
            dt = objHomeDAL.GetProductBySubCategory(id, subCatId);
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

        [Route("api/Home/GetProductsByCategory")]
        [HttpGet("{categoryId}")]
       
        public IActionResult GetProdutsByCategoryId(int categoryId)
        {
            dt = objHomeDAL.GetProductByCategory(categoryId);
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

        [Route("api/Home/GetProductSizeById")]
        public JsonResult GetProductSizeById(int id)
        {
            dt = objHomeDAL.GetProductSizeById(id);
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

        [Route("api/Home/GetColorCodeListByProductId")]
        [HttpGet("{id}")]
       
        public IActionResult GetColorCodeListByProductId(int id)
        {
            dt = objHomeDAL.GetColorCodeListByProductId(id);
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

        [Route("api/Home/GetSizeListByProductId")]
        [HttpGet("{id}")]
       
        public IActionResult GetSizeListByProductId(int id)
        {
            dt = objHomeDAL.GetSizeListByProductId(id);
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

        [Route("api/Home/GetSubCategoryByCategoryId")]
        [HttpGet("{id}")]
       
        public IActionResult GetSubCategoryByCategoryId(int id)
        {
            dt = objHomeDAL.GetSubCategoryByCategoryId(id);
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

        [Route("api/Home/GetSubCategoryById")]
        [HttpGet("{id}")]
       
        public IActionResult GetSubCategoryById(int id)
        {
            dt = objHomeDAL.GetSubCategoryById(id);
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

        //Category start
        [Route("api/Home/GetCategory")]
        public JsonResult GetCategory()
        {
            dt = objHomeDAL.GetProductCategory();
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

        //Category start
        [Route("api/Home/GetAllCategory")]
        public JsonResult GetAllCategory(int IsBrand)
        {
            dt = objHomeDAL.GetAllProductCategory(IsBrand);
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


        [Route("api/Home/GetBlog")]
        [HttpGet]
       
        public JsonResult GetBlog()
        {
            dt = objHomeDAL.GetBlog();
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
       
        [Route("api/Home/GetAllBlogs")]
        public JsonResult GetAllBlogs(int Blog_Id)
        {
            dt = objHomeDAL.GetAllBlogs(Blog_Id);
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
        [Route("api/Home/GetBlogById")]
        [HttpGet("{blog_Id}")]
       
        public IActionResult GetBlogById(int blog_Id)
        {
            dt = objHomeDAL.GetBlogById(blog_Id);

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
        //Category End
    }
}
