using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    
    [Authorize]
    [AllowAnonymous]
    public class Student
    {
        public int userid { get; set; }
        public string name { get; set; }
        public int result { get; set; }
        public int status { get; set; }
        public int time { get; set; }
        public string checkdate { get; set; }
    }


    public class ValuesController : ApiController
    {
        //"name":"30512118","result":"0","status":"4","time":"0"
        Student people = new Student { userid = 100, name = "Andy", result = 2, status = 1, time = 0, checkdate = "00000000" };
        string[] data = new string[] { "name", ":" , "30512118", "result", ":" , "2", "status", ":" , "1","time",":","0" };

        string[] ID = new string[] { "result",":", "1","message" ,":", "OK","mid" ,":" ,"123456" };
        // GET api/values
        public IEnumerable<string> Get()
        {
            return data;
        }

        // GET api/values/5
        public IEnumerable<string> Get(int id)
        {
            return ID;
        }

        // POST api/values
        public string Post([FromBody]string value)
        {
            return  "myvalue:"+value;
        }

        // PUT api/values
        public void Put([FromBody]string value)
        {
            /*
            if (value.Length != 6)
            {
                data[1] = "error";
            }
            else
            {
                for (int i = 0; i < 6; i++)
                    data[i] = value[i];
            }
            */
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
