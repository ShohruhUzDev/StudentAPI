using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Backend.Models;

namespace Student_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
       
        public async Task<ActionResult<TeacherDto>> GetStudentByIdTeacher(int id)
        {
            var teache = await _context.Students.Where(i => i.Id == id).Include(d=>d.Teacher).FirstOrDefaultAsync();
            //  var teacjer = await _context.Students.FindAsync(id);
            var teacherdto = new TeacherDto()
            {
                Id = teache.Teacher.Id,
                LastName = teache.Teacher.LastName,
                FirstName = teache.Teacher.FirstName
            };
            if (teache == null)
            {
                return NotFound();
            }

            return  teacherdto;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student stu)
        {
            
            _context.Students.Add(stu);
            await _context.SaveChangesAsync();
            var yangi = new StudentLIst()
            {
                Id = stu.Id,
                FirstName = stu.FirstName
            };
            return Ok(stu);
            //return CreatedAtAction("GetStudent", new { id = stu.Id }, stu);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
