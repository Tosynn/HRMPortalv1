using HRMPortalv1.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace HRMPortalv1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _env;
            public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
            {
                _configuration = configuration;
                _env = env;
            }

            //Get data
            [HttpGet]
            public JsonResult GetEmployee()
            {
                string query = "select EmployeeId, EmployeeName, Department, convert(varchar(10), DateOfJoining, 120) as DateOfJoining, PhotoFileName from dbo.Employee";
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
            public JsonResult GetSingleEmployee(int id)
            {
                string query = "select EmployeeId, EmployeeName, Department, convert(varchar(10), DateOfJoining, 120) as DateOfJoining, PhotoFileName where EmployeeId = @Employeeid";
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
                        myCommand.Parameters.AddWithValue("@EmployeeId", id);
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
            public JsonResult PostEmployee(Employee emp)
            {
                string query = "insert into dbo.Employee (EmployeeName, Department, DateOfJoining, PhotoFileName) values (@EmployeeName,@Department,@DateOfJoining,@PhotoFileName)";
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
                        myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                        myCommand.Parameters.AddWithValue("@Department", emp.Department);
                        myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                        myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
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
            public JsonResult UpdateEmployee(Employee emp)
            {
                string query = "update dbo.Employee set EmployeeName= @EmployeeName, Department=@Department, DateOfJoining=@DateOfJoining, PhotoFileName=@PhotoFileName where EmployeeId=@EmployeeId";
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
                    myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
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
            public JsonResult DeleteEmployee(int id)
            {
                string query = "delete from dbo.Employee where EmployeeId=@EmployeeId";
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
                        myCommand.Parameters.AddWithValue("@EmployeeId", id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }

                }

                return new JsonResult("Deleted Successfully");
            }


        //Save Images
        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;
            
                using(var stream=new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        }
    }
