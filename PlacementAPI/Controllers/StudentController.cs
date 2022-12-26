using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlacementAPI.Services;
using PlacementAPI.Models;

namespace PlacementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentServices _studentServices;
        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudent()
        {
            return await _studentServices.GetAllStudent();
        }
        [HttpGet("{SlNo}")]
        public async Task<ActionResult<Student>> GetSchoolById(int SlNo)
        {
            var prod = await _studentServices.GetStudentById(SlNo);

            if (prod == null)
            {
                return NotFound();
            }
            return prod;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> InsertOrUpdate(int id, Student S)
        {
            if (id != S.Sl_No)
            {
                return BadRequest();
            }
            try
            {
                await _studentServices.InsertOrUpdate(S);

                return CreatedAtAction("GetAllSchool", new { id = S.Sl_No}, S);
            }

            catch (Exception ex)
            {
                if (GetSchoolById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpDelete("{SchoolID}")]
        public async Task<ActionResult<Student>> Delete(int SlNo)
        {
            var prod = await _studentServices.GetStudentById(SlNo);
            if (prod == null)
            {
                return NotFound();
            }
            await _studentServices.Delete(SlNo);
            return prod;
        }
    }
}
