using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using VastraIndiaDAL;
using VastraIndiaWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VastraIndiaWebAPI.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class LookupController : ControllerBase
    {
        DataTable dt = new DataTable();

        LookupDAL lookup = new LookupDAL();

        SaveImageDAL saveImage = new SaveImageDAL();

        //LookupMaster start

        // GET: api/<LookupController>
        [HttpGet]
        [Authorize]
        [Route("api/Lookup/GetLookupMaster")]
        public JsonResult GetLookupMaster()
        {
            dt = lookup.LookupMaster();
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

        // GET api/<LookupController>/5
        [Route("api/Lookup/GetLookupMasterByid")]
        [HttpGet("{id}")]
        
        public IActionResult GetLookupMasterById(int id)
        {
            dt = lookup.GetLookupMasterById(id);
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

        [Route("api/Lookup/InsertLookupDetails")]
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromForm] LookupDetailsModel lookupDetail)
        {
            if (lookupDetail.Lookup_Id == 3)
            {
                var FileName = "";
                if (lookupDetail.formFile!=null)
                {
                    var Ext = System.IO.Path.GetExtension(lookupDetail.formFile.FileName);

                    FileName = lookupDetail.Description + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;
                }

                string docPath = MyServer.MapPath("Vastra");
                var TippingFolderName = Path.Combine(docPath, "assets", "img", "tipping");

                if (!Directory.Exists(TippingFolderName))
                {
                     Directory.CreateDirectory(TippingFolderName);
                }
                dt = lookup.InsertLookupDetail(lookupDetail.Lookup_Id, lookupDetail.Description, FileName);
                var SaveImage = saveImage.SaveImagesAsync(lookupDetail.formFile, FileName, TippingFolderName);

                return new JsonResult("Added Successfully");
            }
            else
            {
                dt = lookup.InsertLookupDetails(lookupDetail.Lookup_Id, lookupDetail.Description, lookupDetail.ColorName);
                return new JsonResult("Added Successfully");
            }
        }

        // POST api/<LookupController>
        [Route("api/Lookup/InsertLookupMaster")]
        [HttpPost("")]
        [Authorize]
        public IActionResult Post([FromBody] LookupMasterModel lookupMaster)
        {
            dt = lookup.InsertLookupMaster(lookupMaster.Lookup_Name);
            return new JsonResult("Added Successfully");
        }

        // PUT api/<LookupController>/5
        [Route("api/Lookup/UpdateLookupMaster")]
        //  [HttpPut("{id}")]
        [HttpPost]
        [Authorize]
        public IActionResult Put([FromBody] LookupMasterModel lookupMaster)
        {
            if (lookupMaster.Lookup_Id != 0)
            {
                ;
                dt = lookup.UpdateLookupMaster(lookupMaster.Lookup_Id, lookupMaster.Lookup_Name);
                return new JsonResult("Updated Successfully");
            }
            return new JsonResult("Lookup_Id is not valid");

        }

        // DELETE api/<LookupController>/5
        [HttpPost("{id}")]
        [Authorize]
        [Route("api/Lookup/DeleteLookupMaster")]
        public JsonResult Delete([FromBody] int Lookup_Id)
        {
            dt = lookup.DeleteLookupMaster(Lookup_Id);
            return new JsonResult("Deleted Successfully");
        }
        //LookupMaster end

        [HttpGet]
        [Authorize]
        [Route("api/Lookup/GetLookupDetails")]
        public JsonResult GetLookupDetails()
        {
            dt = lookup.LookupDetails();
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


        [Route("api/Lookup/GetLookupDetailsById")]
        [HttpGet("{id}")]
        
        public IActionResult GetLookupDetailsById(int id)
        {
            dt = lookup.GetLookupDetailsById(id);
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




        [Route("api/Lookup/UpdateLookupDetails")]
        //  [HttpPut("{id}")]
        [HttpPost]
        [Authorize]
        public IActionResult Put([FromForm] LookupDetailsModel lookupDetail)
        {
            if (lookupDetail.Lookup_Details_Id != 0)
            {
                if (lookupDetail.Lookup_Id == 3)
                {

                    var FileName = "";
                    if (lookupDetail.formFile != null)
                    {
                        var Ext = System.IO.Path.GetExtension(lookupDetail.formFile.FileName);

                        FileName = lookupDetail.Description + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Ext;
                    }
                    
                    string docPath = MyServer.MapPath("Vastra");
                    var TippingFolderName = Path.Combine(docPath, "assets", "img", "tipping");

                    if (!Directory.Exists(TippingFolderName))
                    {
                        Directory.CreateDirectory(TippingFolderName);
                    }

                    dt = lookup.UpdateLookupDetail(lookupDetail.Lookup_Details_Id, lookupDetail.Lookup_Id, lookupDetail.Description, FileName);
                    var SaveImage = saveImage.SaveImagesAsync(lookupDetail.formFile, FileName, TippingFolderName);

                    return new JsonResult("Updated Successfully");
                }
                else
                {
                    dt = lookup.UpdateLookupDetails(lookupDetail.Lookup_Details_Id, lookupDetail.Lookup_Id, lookupDetail.Description, lookupDetail.ColorName);
                    return new JsonResult("Updated Successfully");
                }
            }
                return new JsonResult("LookupDetailId is not valid");
            
        }


        // DELETE api/<LookupController>/5
        [HttpPost]
        [Authorize]
        [Route("api/Lookup/DeleteLookupDetails")]
        public JsonResult DeleteLookupDetails([FromBody] int Lookup_Details_Id)
        {
            dt = lookup.DeleteLookupDetailsById(Lookup_Details_Id);
            return new JsonResult("Deleted Successfully");
        }

        [HttpGet]
        [Authorize]
        [Route("api/Lookup/GetLookupNameDropDown")]
        public JsonResult GetLookupNameDropDown()
        {
            dt = lookup.LookupNameDropDown();
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
        [Route("api/Lookup/GetColor")]
        public JsonResult GetColor()
        {
            dt = lookup.GetColor();
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
        [Route("api/Lookup/GetTipping")]
        public JsonResult GetTipping()
        {
            dt = lookup.GetTipping();
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
        [Route("api/Lookup/GetSize")]
        public JsonResult GetSize()
        {
            dt = lookup.GetSize();
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
        [Route("api/Lookup/GetLookupMasterPagination")]
        public JsonResult GetLookupMasterPagination(int pageNo, int pageSize)
        {
            dt = lookup.GetLookupMasterPagination(pageNo, pageSize);
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
        [Route("api/Lookup/GetLookupMasterCount")]
        public JsonResult GetLookupMasterCount()
        {
            dt = lookup.GetLookupMasterCount();
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

        // Lookup Details Pagination  Start
        [HttpGet]
        [Authorize]
        [Route("api/Lookup/GetLookupDetailsPagination")]
        public JsonResult GetLookupDetailsPagination(int pageNo, int pageSize)
        {
            dt = lookup.GetLookupDetailsPagination(pageNo, pageSize);
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
        [Route("api/Lookup/GetLookupDetailsCount")]
        public JsonResult GetLookupDetailsCount()
        {
            dt = lookup.GetLookupDetailsCount();
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
        // Lookup Details Pagination End

    }
}
