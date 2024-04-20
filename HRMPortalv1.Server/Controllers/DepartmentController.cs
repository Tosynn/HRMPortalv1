using HRMPortalv1.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HRMPortalv1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get data
        [HttpGet]
        public JsonResult GetDepartment()
        {
            string query = "select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult(table);
        }

        //Get Individual data
        [HttpGet("{id}")]
        public JsonResult GetSingleDepartment(int id)
        {
            string query = "select DepartmentName from dbo.Department where DepartmentId = @Departmentid";
            DataTable table = new DataTable();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult(table);
        }

        //Insert data
        [HttpPost]
        public JsonResult PostDepartment(Department dep)
        {
            string query = "insert into dbo.Department values (@DepartmentName)";
            DataTable table = new DataTable();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Added successfully");
        }

        //Update data
        [HttpPut]
        public JsonResult UpdateDepartment(Department dep)
        {
            string query = "update dbo.Department set DepartmentName= @DepartmentName where DepartmentId=@DepartmentId";
            DataTable table = new DataTable();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Updated Successfully");
        }


        //Delete data
        [HttpDelete("{id}")]
        public JsonResult DeleteDepartment(int id)
        {
            string query = "delete from dbo.Department where DepartmentId=@DepartmentId";
            DataTable table = new DataTable();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Deleted Successfully");
        }



    }
}
