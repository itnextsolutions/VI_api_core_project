using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VastraIndiaDAL;
using VastraIndiaWebAPI.Models;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using VastraIndiaWebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VastraIndiaWebAPI.Controllers
{
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        DataTable dt = new DataTable();
        LoginDAL objLogin = new LoginDAL();

        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [Route("api/Login/login")]
        [HttpPost]
        public IActionResult Post([FromBody] LoginModel login)
        {
            dt = objLogin.GetLoginDetail(login.username);

            if (dt.Rows.Count != 0)
            {
                var hashCode = dt.Rows[0]["Vcode"];
                //Password Hasing Process Call LoginHelper Class Method    
                var encodingPasswordString = LoginHelper.EncodePassword(login.password, Convert.ToString(hashCode));

                dt = objLogin.Login(login.username, encodingPasswordString);

                if (dt.Rows.Count != 0)
                {
                    var jwt = new JwtService(_config);
                    var token = jwt.GenerateSecurityToken("fake@email.com");


                    return new JsonResult(new AuthenticatedResponse { Token = token });
                    //return new JsonResult("Success");
                }

                return new JsonResult("Invalid Password");
            }

            return new JsonResult("Invalid UserName & Password");

        }
        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }




    }
}