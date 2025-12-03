using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;

namespace dapper.Controllers
{
    public class EmployeeController:Controller
    {
        private SqlConnection conn;
        private string connstr;
        public EmployeeController(){
        connstr = "Server=182.93.94.30;Database=EMS;User Id=sa;Password=bdnquery;TrustServerCertificate=True;";
        conn = new SqlConnection(connstr);
        public void Index(){
            conn.Open();
            

        }
        }
       
    }
}