using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using VastraIndiaDAL;
using VastraIndiaWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VastraIndiaWebAPI.Controllers
{
    //[Route("api/[controller]")]
    // [ApiController]
    public class CustomerController : ControllerBase
    {
        DataTable dt = new DataTable();
        CustomerDAL customer = new CustomerDAL();
        SaveImageDAL saveImage = new SaveImageDAL();

        // GET: api/<CustomerController>
        [HttpGet]
        [Authorize]
        [Route("api/Customer/GetCustomerReview")]
        public JsonResult GetCustomerReview()
        {
            dt = customer.GetCustomerReview();
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

        // GET api/<CustomerController>/5
        [Route("api/Customer/GetCustomerReviewByid")]
        [HttpGet("{id}")]
        public IActionResult GetCustomerReviewById(int id)
        {
            dt = customer.GetCustomerReviewById(id);
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



        // POST api/<CustomerController>
        [Route("api/Customer/InsertCustomerReview")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SaveCustReview([FromForm] CustomerModel cust)
        {
            var FileName = "";
            if (cust.formFile != null)
            {
                var Ext = System.IO.Path.GetExtension(cust.formFile.FileName);

                FileName = cust.Client_Name + "_" + DateTime.Now.ToString("dd-MM-yyyy-hh") + Ext;
            }

            string docPath = MyServer.MapPath("Vastra");
            var ClientReviewsFolderName = Path.Combine(docPath, "assets", "img", "client_reviews");

            if (!Directory.Exists(ClientReviewsFolderName))
            {
   
                Directory.CreateDirectory(ClientReviewsFolderName);
            }

            dt = customer.InsertCustomerReview(cust.Client_Name, cust.Profession, cust.Review, FileName, cust.Rating);

            var SaveImage = saveImage.SaveImagesAsync(cust.formFile, FileName, ClientReviewsFolderName);

            return new JsonResult("Added Successfully");
        }

        // PUT api/<CustomerController>/5
        [Route("api/Customer/UpdateCustomerReview")]
        //  [HttpPut("{id}")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateCustReview([FromForm] CustomerModel cust)
        {

            var FileName = "";
            if (cust.formFile != null)
            {
                var Ext = System.IO.Path.GetExtension(cust.formFile.FileName);

                FileName = cust.Client_Name + "_" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm") + Ext;
            }

            
            string docPath = MyServer.MapPath("Vastra");
            var ClientReviewsFolderName = Path.Combine(docPath, "assets", "img", "client_reviews");

            if (!Directory.Exists(ClientReviewsFolderName))
            {
               
                Directory.CreateDirectory(ClientReviewsFolderName);
            }

            if (FileName != null && FileName != "")
            {
                dt = customer.UpdateCustomerReview(cust.Customer_Review_Id, cust.Client_Name, cust.Profession, cust.Review, FileName, cust.Rating);
                var SaveImage = saveImage.SaveImagesAsync(cust.formFile, FileName, ClientReviewsFolderName);
            }

            if (cust.update_imageName != null && cust.update_imageName != "")
            {
                dt = customer.UpdateCustomerReview(cust.Customer_Review_Id, cust.Client_Name, cust.Profession, cust.Review, cust.update_imageName, cust.Rating);
                var SaveImage = saveImage.SaveImagesAsync(cust.formFile, cust.update_imageName, ClientReviewsFolderName);
            }
                return new JsonResult("Updated Successfully");

        }


        // DELETE api/<CustomerController>/5
        [Route("api/Customer/DeleteCustomerReview")]
        [HttpPost]
        [Authorize]
        // DELETE api/<ProductController>/5
        public JsonResult Delete([FromBody] int Customer_Review_Id)
        {

            dt = customer.DeleteCustomerReviewById(Customer_Review_Id);
            return new JsonResult("Deleted Successfully");
        }

        [HttpGet]
        [Authorize]
        [Route("api/Customer/GetCustomerReviewPagination")]
        public JsonResult GetCustomerReviewPagination(int pageNo, int pageSize)
        {
            dt = customer.GetCustomerReviewPage(pageNo, pageSize);
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
        [Authorize]
        [Route("api/Customer/GetCustomerReviewCount")]
        public JsonResult GetCustomerReviewCount()
        {
            dt = customer.GetCustomerReviewCount();
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
    }
}