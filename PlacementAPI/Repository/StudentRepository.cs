using Dapper;
using Microsoft.Extensions.Configuration;
using PlacementAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Repository
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<List<Student>> GetAllStudent()
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Action", "FillTable");
            var lstsch = cn.Query<Student>("SP_Admin_Student_OP", param, commandType: CommandType.StoredProcedure).ToList();
            return lstsch;
        }
        public async Task<List<Branch>> GetAllBranch()
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Action", "FillBranch");
            var lstdis = cn.Query<Branch>("SP_Admin_Student_OP", param, commandType: CommandType.StoredProcedure).ToList();
            return lstdis;
        }
        public async Task<List<Department>> GetAllDepartment(int BranchID)
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@BranchID", BranchID);
            param.Add("@Action", "FillDepartment");
            var lstdis = cn.Query<Department>("SP_Admin_Student_OP", param, commandType: CommandType.StoredProcedure).ToList();
            return lstdis;
        }
        public async Task<Student> GetStudentById(int SlNo)
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Sl_No", SlNo);
            param.Add("@action", "SelectOne");
            var lstsch = cn.Query<Student>("SP_Admin_Student_OP", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            cn.Close();
            return lstsch;
        }

        public async Task<int> InsertOrUpdate(Student S)
        {
            try
            {
                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@Sl_No", S.Sl_No);
                param.Add("@Name", S.Name);
                param.Add("@DOB", S.DOB);
                param.Add("@BranchID", S.BranchID);
                param.Add("@DepartmentID", S.DepartmentID);
                param.Add("@Passing_Year", S.Passing_Year);
                param.Add("@CGPA", S.CGPA);
                param.Add("@BackLog", S.BackLog);
                param.Add("@Action", "InsertOrUpdateStudent");
                int x = cn.Execute("SP_Admin_Student_OP", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return x;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> Delete(int SlNo)
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Sl_No", SlNo);
            param.Add("@Action", "Delete");
            int x = cn.Execute("SP_Admin_Student_OP", param, commandType: CommandType.StoredProcedure);
            cn.Close();
            return x;
        }
    }
}
