using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Task3.Models;

namespace Task3.Controllers
{
    public class DepartmentController : Controller
    {
        private string connstr;
        public DepartmentController(){
            connstr = "Server = 182.93.94.30;Database = EMS;User Id = sa;Password = bdnquery;TrustServerCertificate = True;";
        }
        public IActionResult Index(){
            using (var conn = new SqlConnection(connstr)){
               conn.Open();
               string sql = "SELECT DepId,DepName FROM Department";
               var data = conn.Query(sql).ToList();
               return View(data);
            }
        }
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dep){
            if (ModelState.IsValid)
            {
             using (var conn = new SqlConnection(connstr)){
                conn.Open();
                string sql = @"INSERT INTO Department (DepName) 
                VALUES (@DepName)";
                var data = conn.Execute(sql,new{@DepName=dep.DepName});
                return RedirectToAction("Index","Department");
            }
            }
            return View();
           
        }

        public IActionResult Edit(int id){
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                string sql = "SELECT DepId,DepName FROM Department WHERE DepId = @id";
                var data = conn.QuerySingleOrDefault<Department>(sql,new{id = id});
                return View(data);
            }
        }

        [HttpPost]
        public IActionResult Edit(Department dep){
            if (ModelState.IsValid)
            {
                using (var conn = new SqlConnection(connstr)){
                    conn.Open();
                    string sql = @"UPDATE Department SET DepName = @DepName WHERE DepId = @DepId";
                    var data = conn.Execute(sql,new{@DepName = dep.DepName,@DepId = dep.DepId });
                    return RedirectToAction("Index", "Department");
                }
                
            }
            return View();
        }

        public IActionResult Delete(int id){
            using ( var conn = new SqlConnection(connstr)){
                conn.Open();
                string sql = @"DELETE FROM Department WHERE DepId = @id";
                var data = conn.Execute(sql,new{id = id});
                return RedirectToAction("Index","Department");
            }
        }
    }
}