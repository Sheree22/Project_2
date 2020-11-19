using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShereeGreeffWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ShereeGreeffWebApp.Controllers
{
    public class Emp : Controller
    {
        private readonly ApplicationDbContext _db;

        public Emp(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            var displaydata = _db.EmployeeTable.ToList();
            return View(displaydata);
        }
        //methods for http get and http post
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewEmpClass mvc)
        {
            if(ModelState.IsValid)
            {
                _db.Add(mvc);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mvc);
        }
        //methods for edit
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null) //if employee id is not found redirect id page to index view page
            {
                return RedirectToAction("Index");
            }
            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            return View(getempdetails);
        }
        //http post method to update employee details
        [HttpPost]
        public async Task<IActionResult> Edit(NewEmpClass mc)
        {
            if(ModelState.IsValid)
            {
                //once record is updated save changes and redirect to index page
                _db.Update(mc);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //if record fails to update
            return View(mc);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) //if employee id is not found redirect id page to index view page
            {
                return RedirectToAction("Index");
            }
            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            return View(getempdetails);
        }
        //Delete http get and http post
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) //if employee id is not found redirect id page to index view page
            {
                return RedirectToAction("Index");
            }
            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            return View(getempdetails);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            //implement remove method after getting details
            _db.EmployeeTable.Remove(getempdetails);
            //save changes
            await _db.SaveChangesAsync();
            //redirect to index view page after deleting record

            return RedirectToAction("Index");
        }
    }
}
