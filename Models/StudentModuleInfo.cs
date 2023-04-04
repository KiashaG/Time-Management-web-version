using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROG6212POE.Models
{
    public class StudentModuleInfo
    {
        public double SelfStudyHours { get; set; }

        public string Code { get; set; }
        [Required]
        public string Module_Name { get; set; }
        [Required]
        public int Number_of_Credits{ get; set; }
        [Required]
        public int Class_hours_per_week { get; set; }
        [Required]
        public int Number_of_weeks { get; set; }
        [Required]
        public string start_date { get; set; }
        [Required]
        public string Day_Of_Week { get; set; }


        public List<string> Days_Of_Week { get; set; }
        public static List<string> AvailableDays_Of_Week = new List<string>();


        // LIST TO STORE STUDENT MODULE INFO AND DISPLAY OUTPUT IN SUMMARY 
        public static List<StudentModuleInfo> studentOutput = new List<StudentModuleInfo>();

        // METHOD TO CHECK IF THE CODE IS IN THE SYSTEM
        public static void studentmodule(string code, string moduleName, int numberofcredits, int classhoursperweek, int numberofweeks,string startDate, string dayofweek)
        {
            Boolean found = false;
            LogicDAL logic = new LogicDAL();
          StudentModuleInfo stud= new StudentModuleInfo();
            stud.Code = code;
            stud.Module_Name = moduleName;
            stud.Number_of_Credits = numberofcredits;
            stud.Class_hours_per_week = classhoursperweek;
            stud.Number_of_weeks = numberofweeks;
            stud.start_date = startDate;
            stud.Day_Of_Week = dayofweek;

            foreach (var item in studentOutput)
            {
                if (item.Code.Equals(code)) 
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                logic.AddStudentModule(stud);
            }

            found = false;
            SummaryInfo sum = new SummaryInfo();
            foreach (var item in sum.summary())
            {
                if (item.Code.Equals(code))
                {
                    found = true;
                    break;
                }

            }

            if (!found)
            {
                logic.AddREGISTER_STUDENT_HOURS(LoginInfo.currentuser, code);
            }
 
        }

        public void setdaysofweek()
        {
            // call logic dal 
            // return list of used days
            // determine other days
            // update days of week list
        }

    }
}
