using System;
using Dapper;
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
        }

        public IActionResult Index(){
            conn.Open();
            string sql = "SELECT Eid,Name,Email,Phone,Gender,DepName AS DepName,DName AS DesName FROM Employee e INNER JOIN Department d ON e.DepID = d.DepId INNER JOIN Designation d2 ON e.Did = d2.Did";
            var employees = conn.Query(sql).ToList();
            return View(employees);
            conn.Close();
        }

       /* public IActionResult Edit(int id){
            conn.Open();
            string sql = "SELECT Eid,Name,Email,Phone,Gender,DepName AS DepName,DName AS DesName FROM Employee e
             INNER JOIN Department d ON e.DepID = d.DepId 
             INNER JOIN Designation d2 ON e.Did = d2.Did WHERE Eid = @id "
             var employees = conn.Query(sql).ToList();
             return View(employees);
            conn.Close();
        }*/
       
    }
}