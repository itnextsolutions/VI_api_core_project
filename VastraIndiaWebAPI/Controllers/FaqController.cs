using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using VastraIndiaDAL;
using VastraIndiaWebAPI.Models;

namespace VastraIndiaWebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class FaqController : ControllerBase
    {
        DataTable dt = new DataTable();
        FaqDAL objfaq = new FaqDAL();

        [HttpGet]
        [Authorize]
        [Route("api/Faq/GetAllFaqPagination")]
        public JsonResult GetAllFaqPagination(int pageNo, int pageSize)
        {
            dt = objfaq.GetFaqPagination(pageNo, pageSize);
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
       
        [Route("api/Faq/GetFaqCount")]
        public JsonResult GetAlFaqCount()
        {
            dt = objfaq.GetFaqCount();
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

        // POST api/<FaqController>
        [Route("api/Faq/InsertFaq")]
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] FaqModel faq)
        {
            dt = objfaq.InsertFaq(faq.Question, faq.Answer);
            return new JsonResult("Added Successfully");
        }
        // PUT api/<FaqController>/5
        [Route("api/Faq/UpdateFaq")]
        [Authorize]
        //  [HttpPut("{id}")]
        [HttpPost]
        public IActionResult Put([FromBody] FaqModel faq)
        {
            if (faq.Id != 0)
            {
                dt = objfaq.updateFaq(faq.Id, faq.Question, faq.Answer);
                return new JsonResult("Updated Successfully");
            }

            return new JsonResult("Data is not valid");

        }

        // DELETE api/<FaqController>/5
        [Route("api/Faq/Delete")]
        [HttpPost]
        public JsonResult Delete([FromBody] int Id)
        {
            dt = objfaq.DeleteFaq(Id);
            return new JsonResult("Deleted Successfully");
        }
    }
}
