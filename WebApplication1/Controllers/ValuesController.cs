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
            //int count = 0;
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
               var count = sqlite_cmd.ExecuteScalar();
                if (count == DBNull.Value) count = 0;
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
                var count = sqlite_cmd.ExecuteScalar();
                if (count == DBNull.Value) count = 0;
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
        // GET api/<ValuesController>/5
        [HttpGet("getmax/speed={speedQuery}&accel={accelQuery}&weight={weightQuery}&handling={handlingQuery}&traction{tractionQuery}&best{bestQuery}&count={countQuery}")]
        public IActionResult GetMax(string speedQuery, string accelQuery, string weightQuery, string handlingQuery, string tractionQuery, string bestQuery, string countQuery)
        {
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");

            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            List<double> _data = new List<double>();

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT MAX(SPEED) FROM MarioTable WHERE " +
                "SPEED >='" + speedQuery + "' AND " +
                "ACCEL >='" + accelQuery + "' AND " +
                "WEIGHT >='" + weightQuery + "' AND " +
                "BEST >='" + bestQuery + "' AND " +
                "TRACTION >='" + tractionQuery + "' AND " +
                "HANDLING >='" + handlingQuery + "'";

           var maxSpeed = sqlite_cmd.ExecuteScalar();
            if (maxSpeed == DBNull.Value) maxSpeed = 0;


            sqlite_cmd.CommandText = "SELECT MAX(ACCEL) FROM MarioTable WHERE " +
             "SPEED >='" + speedQuery + "' AND " +
             "ACCEL >='" + accelQuery + "' AND " +
             "WEIGHT >='" + weightQuery + "' AND " +
             "BEST >='" + bestQuery + "' AND " +
             "TRACTION >='" + tractionQuery + "' AND " +
             "HANDLING >='" + handlingQuery + "'";
           var maxAccel = sqlite_cmd.ExecuteScalar() ;
            if (maxAccel == DBNull.Value) maxAccel = 0;

            sqlite_cmd.CommandText = "SELECT MAX(WEIGHT) FROM MarioTable WHERE " +
              "SPEED >='" + speedQuery + "' AND " +
              "ACCEL >='" + accelQuery + "' AND " +
              "WEIGHT >='" + weightQuery + "' AND " +
              "BEST >='" + bestQuery + "' AND " +
              "TRACTION >='" + tractionQuery + "' AND " +
              "HANDLING >='" + handlingQuery + "'";
           var maxWeight = sqlite_cmd.ExecuteScalar() ;
            if (maxWeight == DBNull.Value) maxWeight = 0;


            sqlite_cmd.CommandText = "SELECT MAX(HANDLING) FROM MarioTable WHERE " +
              "SPEED >='" + speedQuery + "' AND " +
              "ACCEL >='" + accelQuery + "' AND " +
              "WEIGHT >='" + weightQuery + "' AND " +
              "BEST >='" + bestQuery + "' AND " +
              "TRACTION >='" + tractionQuery + "' AND " +
              "HANDLING >='" + handlingQuery + "'";
           var maxHandling = sqlite_cmd.ExecuteScalar() ;
            if (maxHandling == DBNull.Value) maxHandling = 0;

            sqlite_cmd.CommandText = "SELECT MAX(TRACTION) FROM MarioTable WHERE " +
              "SPEED >='" + speedQuery + "' AND " +
              "ACCEL >='" + accelQuery + "' AND " +
              "WEIGHT >='" + weightQuery + "' AND " +
              "BEST >='" + bestQuery + "' AND " +
              "TRACTION >='" + tractionQuery + "' AND " +
              "HANDLING >='" + handlingQuery + "'";
           var maxTraction = sqlite_cmd.ExecuteScalar();
            if (maxTraction == DBNull.Value) maxTraction = 0;

            sqlite_cmd.CommandText = "SELECT MAX(BEST) FROM MarioTable WHERE " +
              "SPEED >='" + speedQuery + "' AND " +
              "ACCEL >='" + accelQuery + "' AND " +
              "WEIGHT >='" + weightQuery + "' AND " +
              "BEST >='" + bestQuery + "' AND " +
              "TRACTION >='" + tractionQuery + "' AND " +
              "HANDLING >='" + handlingQuery + "'";
           var maxBest = sqlite_cmd.ExecuteScalar();
            if (maxBest == DBNull.Value) maxBest = 0;



            Response.Headers.Add("maxspeed", maxSpeed.ToString());
            Response.Headers.Add("maxaccel", maxAccel.ToString());
            Response.Headers.Add("maxweight", maxWeight.ToString());
            Response.Headers.Add("maxhandling", maxHandling.ToString());
            Response.Headers.Add("maxtraction", maxTraction.ToString());
            Response.Headers.Add("maxbest", maxBest.ToString());



            return Ok(maxBest.ToString());

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
