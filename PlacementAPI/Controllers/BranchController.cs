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
    public class BranchController : Controller
    {
        private readonly IStudentServices _studentServices;
        public BranchController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<Branch>>> GetAllBranch()
        {
            return await _studentServices.GetAllBranch();
        }
    }
}
