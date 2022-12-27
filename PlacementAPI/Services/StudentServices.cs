using PlacementAPI.Models;
using PlacementAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<List<Student>> GetAllStudent()
        {
            return await _studentRepository.GetAllStudent();
        }
        public async Task<List<Branch>> GetAllBranch()
        {
            return await _studentRepository.GetAllBranch();
        }
        public async Task<List<Department>> GetAllDepartment(int BranchID)
        {
            return await _studentRepository.GetAllDepartment(BranchID);
        }
        public async Task<Student> GetStudentById(int SlNo)
        {
            return await _studentRepository.GetStudentById(SlNo);
        }
        public async Task<int> InsertOrUpdate(Student S)
        {
            return await _studentRepository.InsertOrUpdate(S);
        }
        public async Task<int> Delete(int SlNo)
        {
            return await _studentRepository.Delete(SlNo);
        }
    }
}
