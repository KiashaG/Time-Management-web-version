using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROG6212POE.Models
{
    class LogicDAL
    {
        string username; 
        string connectionStringDEV = "Data Source=KIASHA-LAPTOP-H;Initial Catalog=STUDENTDB;Integrated Security=True";

        // GET REGISTERSTUDENTHOURS 
        public IEnumerable<SummaryInfo> GetRegisterStudentHours(string username)
        {
         
            List<SummaryInfo> moduleList = new List<SummaryInfo>();
            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_GetRegisterStudentHours", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", username);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   SummaryInfo mod = new SummaryInfo();
                    mod.Code = dr["CODE"].ToString();
                    mod.User_Name = dr["USERS_NAME"].ToString();
                    moduleList.Add(mod);
              
                }
                con.Close();
            }
            return moduleList;
        }


        // GET REGISTER BY ID
        public RegisterInfo GetRegisterById(string Register_ID)
        {
            RegisterInfo reg = new RegisterInfo();

            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_GetRegisterById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@REGISTERID", Register_ID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                  
                   reg.User_Name = dr["USERS_NAME"].ToString();
                   reg.User_Email = dr["USERS_EMAIL"].ToString();
                   reg.User_Password  = dr["USERS_PASSWORD"].ToString();

                }
                con.Close();
            }
            return reg;
        }


        // GET STUDENT MODULE  BY ID
        public StudentModuleInfo GetStudentModuleByID(string STUDENTMODULE_ID)
        {
            StudentModuleInfo stm = new StudentModuleInfo();

            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_GetStudentModuleByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CODE ", STUDENTMODULE_ID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    stm.Code = dr["CODE"].ToString();
                    stm.Module_Name = dr["MODULE_NAME"].ToString();
                    stm.Number_of_Credits = Convert.ToInt32(dr["NUMBER_OF_CREDITS"].ToString());
                    stm.Class_hours_per_week = Convert.ToInt32(dr["CLASS_HOURS_PER_WEEK"].ToString());
                    stm.Number_of_weeks = Convert.ToInt32(dr["NUMBER_OF_WEEKS"].ToString());
                    stm.start_date = dr["START_DATE"].ToString();
                    stm.Day_Of_Week = dr["DAY_OF_WEEK"].ToString();

                }
                con.Close();
            }
            return stm;
        }

        // GET ALL REGISTERS
        public IEnumerable<RegisterInfo> GetAllRegisters()
        {
            List<RegisterInfo> registerList = new List<RegisterInfo>();
            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllRegisters", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   RegisterInfo reg = new RegisterInfo();

                    reg.User_Name = dr["USERS_NAME"].ToString();
                    reg.User_Password  = dr["USERS_PASSWORD"].ToString();

                    registerList.Add(reg);
                }
                con.Close();
            }
            return registerList;
        }

       // GET ALL STUDENT MODULES
        public IEnumerable<StudentModuleInfo> GetAllStudentModules()
        {
            List<StudentModuleInfo> moduleStudentList = new List<StudentModuleInfo>();
            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllModules", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StudentModuleInfo studentMod = new StudentModuleInfo();

                    studentMod.Code = dr["CODE"].ToString();
                    studentMod.Module_Name = dr["MODULE_NAME"].ToString();
                    studentMod.Number_of_Credits = Convert.ToInt32(dr["NUMBER_OF_CREDITS"].ToString());
                    studentMod.Class_hours_per_week = Convert.ToInt32(dr["CLASS_HOURS_PER_WEEK"].ToString());
                    studentMod.Number_of_weeks = Convert.ToInt32(dr["NUMBER_OF_WEEKS"].ToString());
                    studentMod.start_date = dr["START_DATE"].ToString();
                    studentMod.Day_Of_Week = dr["DAY_OF_WEEK"].ToString();

                    moduleStudentList.Add(studentMod);
                }
                con.Close();
            }
            return moduleStudentList;
        }

        // INSERT REGISTER
        public void AddREGISTER(RegisterInfo reg)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertRegisters", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@USERS_NAME", reg.User_Name);
                cmd.Parameters.AddWithValue("@USERS_EMAIL", reg.User_Email);
                cmd.Parameters.AddWithValue("@USERS_PASSWORD",reg.User_Password);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        // INSERT REGISTERSTUDENTHOURS
        public void AddREGISTER_STUDENT_HOURS(string username,string code)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertRegisterStudentHours", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CODE", code);
                cmd.Parameters.AddWithValue("@USERS_NAME", username);
             
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        // INSERT STUDENTMODULE
        public void AddStudentModule(StudentModuleInfo stud)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertStudentModules", con);
                cmd.CommandType = CommandType.StoredProcedure;
                

                cmd.Parameters.AddWithValue("@CODE", stud.Code);
                cmd.Parameters.AddWithValue("@MODULE_NAME", stud.Module_Name);
                cmd.Parameters.AddWithValue("@NUMBER_OF_CREDITS", stud.Number_of_Credits);
                cmd.Parameters.AddWithValue("@CLASS_HOURS_PER_WEEK", stud.Class_hours_per_week);
                cmd.Parameters.AddWithValue("@NUMBER_OF_WEEKS", stud.Number_of_weeks);
                cmd.Parameters.AddWithValue("@START_DATE", stud.start_date);
                cmd.Parameters.AddWithValue("@DAY_OF_WEEK", stud.Day_Of_Week);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

    
            }
        }

        // INSERT STUDYHOURS
        public void AddStudyHours(StudyHoursInfo studyHours)
        {
            using (SqlConnection con = new SqlConnection(connectionStringDEV))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertStudyHour", con);
                cmd.CommandType = CommandType.StoredProcedure;
              

                cmd.Parameters.AddWithValue("@MODULE_STUDIED", studyHours.Module_Studied);
                cmd.Parameters.AddWithValue("@NUMBER_OF_CREDITS", studyHours.Number_of_credits);
                cmd.Parameters.AddWithValue("@CLASS_HOURS_PER_WEEK", studyHours.Class_hours_per_week);
                cmd.Parameters.AddWithValue("@DATE_STUDIED", studyHours.Date_Studied);
                cmd.Parameters.AddWithValue("@NUMBER_OF_WEEKS", studyHours.Number_of_weeks);
           
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

      
            }
        }

    }
}

