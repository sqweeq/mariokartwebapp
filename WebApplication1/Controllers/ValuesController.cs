using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("speed={speedQuery}&accel={accelQuery}&weight={weightQuery}&handling={handlingQuery}&traction{tractionQuery}&best{bestQuery}&count={countQuery}")]
        public IActionResult Get(string speedQuery, string accelQuery, string weightQuery, string handlingQuery, string tractionQuery, string bestQuery, string countQuery ) 
        {

            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");

            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            string res = "";
            int count = 0;
            // if countquery is true then get count only, if false then also get each combos stats to return
            if (countQuery == "true") {
                res = "count was true";
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT COUNT(*) FROM MarioTable WHERE " +
                    "SPEED >='" + speedQuery + "' AND " +
                    "ACCEL >='" + accelQuery + "' AND " +
                    "WEIGHT >='" + weightQuery + "' AND " +
                    "BEST >='" + bestQuery + "' AND " +
                    "TRACTION >='" + tractionQuery + "' AND " +

                    "HANDLING >='" + handlingQuery + "'";
                count = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                Response.Headers.Add("count", count.ToString());
              
                return Ok(count.ToString());

            }
            else
            {
                // get count to return in custom header
                res = "count was false";
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT COUNT(*) FROM MarioTable WHERE " +
                    "SPEED >='"+ speedQuery+ "' AND " +
                    "ACCEL >='" + accelQuery + "' AND " +
                    "WEIGHT >='" + weightQuery + "' AND " +
                    "TRACTION >='" + tractionQuery + "' AND " +
                    "BEST >='" + bestQuery + "' AND " +
                    "HANDLING >='" + handlingQuery + "'";
                count = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                //get data of those filtered combo rows
                sqlite_cmd.CommandText = "SELECT * FROM MarioTable WHERE " +
                  "SPEED >='" + speedQuery + "' AND " +
                  "ACCEL >='" + accelQuery + "' AND " +
                  "WEIGHT >='" + weightQuery + "' AND " +
                  "TRACTION >='" + tractionQuery + "' AND " +
                  "BEST >='" + bestQuery + "' AND " +
                  "HANDLING >='" + handlingQuery + "' ORDER BY BEST DESC LIMIT 25";
                SQLiteDataReader dataReader = sqlite_cmd.ExecuteReader();
                DataTable dt = new DataTable("MarioTable");
                dt.Load(dataReader);
                Response.Headers.Add("count", count.ToString());
                string jsonString = string.Empty;
                jsonString = JsonConvert.SerializeObject(dt);

                return Ok(jsonString);

            }
            return Ok("ok");
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
