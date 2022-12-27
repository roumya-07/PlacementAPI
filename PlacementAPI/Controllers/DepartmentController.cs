using Microsoft.AspNetCore.Mvc;
using PlacementAPI.Models;
using PlacementAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IStudentServices _studentServices;
        public DepartmentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        [HttpGet("{BranchID}")]
        public async Task<ActionResult<List<Department>>> GetAllDepartment(int BranchID)
        {
            return await _studentServices.GetAllDepartment(BranchID);
        }
    }
}
