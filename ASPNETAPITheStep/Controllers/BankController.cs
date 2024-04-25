using ASPNETAPITheStep.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETAPITheStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<BankBranchResponse> GetAll() {

            return _context.BankBranches.Select(b=> new BankBranchResponse
            {
                BranchManager = b.BranchManager,
                Location = b.Location,
                Name = b.Name
            }).ToList();
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
        public IActionResult Delete(int id)
        {
            var branch = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(branch);
            _context.SaveChanges();

            return Ok();
        }

    }
}
