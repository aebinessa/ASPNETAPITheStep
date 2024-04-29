using ASPNETAPITheStep.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ASPNETAPITheStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        // Admin only actions
    }

    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10, string searchTerm = "")
        {
            var query = _context.BankBranches.Select(b => new BankBranchResponse
            {
                BranchManager = b.BranchManager,
                Location = b.Location,
                Name = b.Name
            });

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Name.Contains(searchTerm) ||
                                          b.BranchManager.Contains(searchTerm) ||
                                          b.Location.Contains(searchTerm));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            return Ok(new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = items
            });
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var bank = _context.BankBranches.Find(id);
            if (bank == null) 
            { 
                return NotFound();
            }
            return Ok( new BankBranchResponse
            {
                BranchManager = bank.BranchManager,
                Location = bank.Location,
                Name = bank.Name,
                Employees = bank.Employees.ToList()

        });
        
        }

        [HttpPost]
        public IActionResult AddBranch(AddBranchRequest request)
        {
            var branch = new BankBranch() 
            { 
                BranchManager = request.BranchManager, 
                Location = request.Location,
                Name = request.Name};
            _context.BankBranches.Add(branch);
            _context.SaveChanges();
            return Created(nameof(Details), new {Id = branch.Id});
        }
        [HttpPatch("{id}")]
        public IActionResult Edit(int id, EditBranchRequest request)
        {
            var branch = _context.BankBranches.Find(id);
            branch.BranchManager = request.BranchManager;
            branch.Location = request.Location;
            branch.Name = request.Name;
            
            _context.SaveChanges();
            return Created(nameof(Details), new { Id = branch.Id });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var branch = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(branch);
            _context.SaveChanges();

            return Ok();
        }

    }
}
