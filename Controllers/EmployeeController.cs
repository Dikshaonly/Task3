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
        private string connstr;
        public EmployeeController(){
        connstr = "Server=182.93.94.30;Database=EMS;User Id=sa;Password=bdnquery;TrustServerCertificate=True;";
        }

        public IActionResult Index(){
            using (var conn = new SqlConnection(connstr)){
            conn.Open();
            string sql = @"SELECT Eid,Name,Email,Phone,Gender,
            DepName AS DepName,DName AS DesName 
            FROM Employee e
             INNER JOIN Department d ON e.DepID = d.DepId 
             INNER JOIN Designation d2 ON e.Did = d2.Did";
            var employees = conn.Query(sql).ToList();
            return View(employees);
            }
        }
        //[GET]
        public IActionResult Edit(int id){
            using (var conn = new SqlConnection(connstr)){
            conn.Open();
            string sql = @"SELECT 
            e.Eid,e.Name,e.Email,e.Phone,
            e.Gender,e.DepId,e.Did,
            d.DepName AS DepName,
            d2.Dname AS DesName 
            FROM Employee e 
            INNER JOIN Department d ON e.DepID = d.DepId
             INNER JOIN Designation d2 ON e.Did = d2.Did 
             WHERE e.Eid = @id ";
             var employees = conn.QuerySingleOrDefault<Employee>(sql,new{id = id});
             var departments = conn.Query<Department>("SELECT DepId,DepName FROM Department").ToList();
             var designations = conn.Query<Designation>("SELECT Did, DName FROM Designation").ToList();
             ViewBag.Departments = new SelectList(departments, "DepId", "DepName", employees.DepId);
             ViewBag.Designations = new SelectList(designations, "Did", "Dname", employees.Did);
             return View(employees);
            }
           
        }
        //[POST]
        [HttpPost]
        public IActionResult Edit(Employee employee){
            if (!ModelState.IsValid)
            {
                using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
            var departments = conn.Query<Department>("SELECT DepId,DepName FROM Department").ToList();
             var designations = conn.Query<Designation>("SELECT Did, DName FROM Designation").ToList();
             ViewBag.Departments = new SelectList(departments, "DepId", "DepName", employee.DepId);
             ViewBag.Designations = new SelectList(designations, "Did", "Dname", employee.Did);
            }
            return View(employee);
            }
                
        using (var conn=new SqlConnection(connstr)){
             conn.Open();
            string sql = @"UPDATE Employee SET
             Name = @Name,Email = @Email,Phone = @Phone,Gender = @Gender,
             DepId = @DepId, Did = @Did
             WHERE Eid=@Eid";

             var employees = conn.Execute(sql,new{@Name = employee.Name,@Email = employee.Email, 
             @Phone = employee.Phone, @Gender = employee.Gender,@DepId = employee.DepId ,
             @Did = employee.Did, @Eid = employee.Eid});
             return RedirectToAction("Index");
        }
        }
       
    }
}