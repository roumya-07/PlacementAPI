using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Placement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PlacementAPI.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Placement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        Uri baseAdd = new Uri("http://localhost:10771/api");

        HttpClient client;
        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
            client = new HttpClient();
            client.BaseAddress = baseAdd;
        }
        public async Task<IActionResult> Index()
        {
            List<Branch> lstcat = new List<Branch>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Branch").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstcat = JsonConvert.DeserializeObject<List<Branch>>(data);
                lstcat.Insert(0, new Branch { BranchID = 0, BranchName = "Select One" });
                ViewBag.Branch = lstcat;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(Student S)
        {
            string data = JsonConvert.SerializeObject(S);
            HttpResponseMessage response;
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            response = client.PutAsync(client.BaseAddress + "/Student/" + S.Sl_No, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<JsonResult> GetStudent()
        {
            string data = null;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Student").Result;
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }
            return Json(data);
        }
        public async Task<JsonResult> Department_Bind(int BranchID)
        {
            string data = null;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department/" + BranchID).Result;
            List<SelectListItem> scalist = new List<SelectListItem>();
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                var lstscat = JsonConvert.DeserializeObject<List<Department>>(data);
                foreach (Department dr in lstscat)
                {
                    scalist.Add(new SelectListItem { Text = dr.DepartmentName, Value = dr.DepartmentID.ToString() });
                }
            }
            var jsonres = JsonConvert.SerializeObject(scalist);
            return Json(jsonres);
        }
        public async Task<JsonResult> Edit(int Sl_No)
        {
            Student schlst = new Student();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Student/" + Sl_No).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                schlst = JsonConvert.DeserializeObject<Student>(data);
            }
            var jsonres = JsonConvert.SerializeObject(schlst);
            return Json(jsonres);
        }

        public int Delete(int Sl_No)
        {
            Student schlst = new Student();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Student/" + Sl_No).Result;
            if (response.IsSuccessStatusCode)
            {
                return 1;

            }
            return 0;
        }
    }
}