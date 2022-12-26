using PlacementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Repository
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllStudent();
        public Task<List<Branch>> GetAllBranch();
        public Task<Student> GetStudentById(int SlNo);
        public Task<int> InsertOrUpdate(Student S);
        public Task<int> Delete(int SlNo);
    }
}
