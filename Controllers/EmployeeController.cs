using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Task3.Models;

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
            string sql = @"SELECT Eid,Name,Email,Phone,Gender,
            DepName AS DepName,DName AS DesName 
            FROM Employee e
             INNER JOIN Department d ON e.DepID = d.DepId 
             INNER JOIN Designation d2 ON e.Did = d2.Did";
            var employees = conn.Query(sql).ToList();
            return View(employees);
            conn.Close();
        }
        //[GET]
        public IActionResult Edit(int id){
            conn.Open();
            string sql = @"SELECT 
            e.Eid,e.Name,e.Email,e.Phone,
            e.Gender,e.DepId,e.Did,
            d.DepName AS DepName,
            d2.DName AS DesName 
            FROM Employee e 
            INNER JOIN Department d ON e.DepID = d.DepId
             INNER JOIN Designation d2 ON e.Did = d2.Did 
             WHERE Eid = @id ";
             var employees = conn.QuerySingleOrDefault<Employee>(sql,new{id = id});
             var departments = conn.Query<Department>("SELECT * FROM Department").ToList();
             var designations = conn.Query<Designation>("SELECT * FROM Designation").ToList();
             ViewBag.Departments = new SelectList(departments, "DepId", "DepName", employees.DepId);
             ViewBag.Designations = new SelectList(designations, "Did", "DName", employees.Did);
             return View(employees);
            conn.Close();
        }
        //[POST]
        /*[HttpPost]
        public IActionResult Edit(Employee employee,int id){
            conn.Open();
            string sql = @"UPDATE employee SET
             Name = @Name,Email = @Email,Phone = @Phone,Gender = @Gender,
             DepName = @DepName,
             ";
        }*/
       
    }
}