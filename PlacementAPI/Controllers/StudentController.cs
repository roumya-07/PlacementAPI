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
        [HttpGet("{Sl_No}")]
        public async Task<ActionResult<Student>> GetSchoolById(int Sl_No)
        {
            var prod = await _studentServices.GetStudentById(Sl_No);

            if (prod == null)
            {
                return NotFound();
            }
            return prod;
        }
        [HttpPut("{Sl_No}")]
        public async Task<ActionResult<Student>> InsertOrUpdate(int Sl_No, Student S)
        {
            if (Sl_No != S.Sl_No)
            {
                return BadRequest();
            }
            try
            {
                await _studentServices.InsertOrUpdate(S);

                return CreatedAtAction("GetAllStudent", new { Sl_No = S.Sl_No}, S);
            }

            catch (Exception ex)
            {
                if (GetSchoolById(Sl_No) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpDelete("{Sl_No}")]
        public async Task<ActionResult<Student>> Delete(int Sl_No)
        {
            var prod = await _studentServices.GetStudentById(Sl_No);
            if (prod == null)
            {
                return NotFound();
            }
            await _studentServices.Delete(Sl_No);
            return prod;
        }
    }
}
