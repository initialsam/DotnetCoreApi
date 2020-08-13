using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetCoreApi.Models;
using Microsoft.Data.SqlClient;

namespace DotnetCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public DepartmentsController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.Where(x => x.IsDeleted == false).ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Department.Where(x => x.DepartmentId == id && x.IsDeleted == false).SingleOrDefaultAsync();

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            try
            {
                department.DateModified = DateTime.Now;
                var res = await _context.Department.FromSqlInterpolated(
                    $"EXEC Department_Update {department.DepartmentId},{ department.Name}, { department.Budget}, {department.StartDate}, {department.InstructorId}, {department.RowVersion}, {department.DateModified}, {department.IsDeleted}")
                     .ToListAsync();
                var updatedDepartment = res.SingleOrDefault();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            department.StartDate = DateTime.Now;
            department.DateModified = DateTime.Now;
            var res = await _context.Department.FromSqlInterpolated(
                $"EXEC Department_Insert { department.Name}, { department.Budget}, {department.StartDate}, {department.InstructorId}, {department.DateModified}, {department.IsDeleted}")
                .ToListAsync();
            var newDepartment = res.SingleOrDefault();

            return CreatedAtAction("GetDepartment", new { id = newDepartment.DepartmentId }, newDepartment);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC Department_Delete {id},{department.RowVersion}");

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}
