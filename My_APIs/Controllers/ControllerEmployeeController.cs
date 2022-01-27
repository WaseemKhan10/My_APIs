using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace My_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerEmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ControllerEmployeeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from  MyAPIsDB.Employee";
            DataTable dt = new DataTable();
            string Sqldatasource = _config.GetConnectionString("AuthenticationDbContextContextConnection");
            MySqlDataReader reader = null;
            using (MySqlConnection con = new MySqlConnection(Sqldatasource))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    con.Close();
                }
            }
            return new JsonResult(dt);
        }
        [HttpPost]
        public JsonResult Post(Models.ModelEmployee EmpOBJ)
        {
            string query = @"insert into MyAPIsDB.Employee (EmployeeName,EmployeePhone,EmployeeAddress,EmployeeDept)
            Values(@EmployeeName,@EmployeePhone,@EmployeeAddress,@EmployeeDept)";
            DataTable table = new DataTable();
            string sqlDatasource = _config.GetConnectionString("AuthenticationDbContextContextConnection");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@EmployeeName", EmpOBJ.EmployeeName);
                    mycmd.Parameters.AddWithValue("@EmployeePhone", EmpOBJ.EmployeePhone);
                    mycmd.Parameters.AddWithValue("@EmployeeAddress", EmpOBJ.EmployeeAddress); ;
                    mycmd.Parameters.AddWithValue("@EmployeeDept", EmpOBJ.EmployeeDept);
                    myreader = mycmd.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Succesfully");
        }

        [HttpPut]
        public JsonResult Put(Models.ModelEmployee EmpOBJ)
        {
            string query = @"Update MyAPIsDB.Employee set EmployeeName=@EmployeeName,EmployeePhone=@EmployeePhone,EmployeeAddress=@EmployeeAddress,EmployeeDept=@EmployeeDeptwhere EmployeeID=@EmployeeID;";
            DataTable table = new DataTable();
            string sqlDatasource = _config.GetConnectionString("AuthenticationDbContextContextConnection");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@EmployeeID", EmpOBJ.EmployeeID);
                    mycmd.Parameters.AddWithValue("@EmployeeName", EmpOBJ.EmployeeName);
                    mycmd.Parameters.AddWithValue("@EmployeePhone", EmpOBJ.EmployeePhone);
                    mycmd.Parameters.AddWithValue("@EmployeeAddress", EmpOBJ.EmployeeAddress);
                    mycmd.Parameters.AddWithValue("@EmployeeDept", EmpOBJ.EmployeeDept);
                    myreader = mycmd.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Updated Succesfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete From MyAPIsDB.Employee where EmployeeID=@EmployeeID;";
            DataTable table = new DataTable();
            string sqlDatasource = _config.GetConnectionString("AuthenticationDbContextContextConnection");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@EmployeeID", id);
                    myreader = mycmd.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deleted Succesfully");
        }
        [HttpGet("{id}")]
        public JsonResult GetbyID(int id)
        {
            string query = @"select * From MyAPIsDB.Employee where EmployeeID=@EmployeeID;";
            DataTable dt = new DataTable();
            string Sqldatasource = _config.GetConnectionString("AuthenticationDbContextContextConnection");
            MySqlDataReader reader = null;
            using (MySqlConnection con = new MySqlConnection(Sqldatasource))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    con.Close();
                }
            }
            return new JsonResult(dt);
        }
    }
}
