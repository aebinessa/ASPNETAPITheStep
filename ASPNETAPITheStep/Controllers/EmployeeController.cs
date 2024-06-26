﻿using ASPNETAPITheStep.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETAPITheStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly BankContext _context;


        public EmployeeController(BankContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<AddEmployeeResponse> GetAll()
        {
            return _context.Employees.Select(e => new AddEmployeeResponse
            {
                Name = e.Name,  
                CivilId = e.CivilId,
                Position = e.Position,
            }).ToList();
        }
        //

        [HttpPost("{id}")]
        public IActionResult AddEmployee(int id, AddEmployeeRequest request)
        {
            var e = _context.BankBranches.Find(id);
            var employee = new Employee()
            {
                Name = request.Name,
                CivilId = request.CivilId,
                Position = request.Position,
                BankBranch = e
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Created(nameof(Details), new { Id = employee.Id });
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var employee = _context.Employees.Include(e => e.BankBranch).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }




        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddEmployeeRequest request)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = request.Name;
            employee.CivilId = request.CivilId;
            employee.Position = request.Position;
            _context.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok();
        }

    }
}
