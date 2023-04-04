using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6212POE.Models
{
   public class StudyHoursInfo
    {
        public string Module_Studied { get; set; }
        [Required]
        public int Number_of_credits { get; set; }
        [Required]
        public int Class_hours_per_week { get; set; }
        [Required]
        public string Date_Studied { get; set; }
        [Required]
        public int Number_of_weeks { get; set; }


        
        //RETRIVING DATA FROM ADD STUDY HOURS STORED PROCEDURE

        public static void studyhours(string moduleStudied, int numberOfcredits, int classHoursperweek, string dateStudied, int numberOfweeks)
        {

            LogicDAL logic = new LogicDAL();
            StudyHoursInfo studyHours = new StudyHoursInfo();
            studyHours.Module_Studied = moduleStudied;
            studyHours.Number_of_credits = numberOfcredits;
            studyHours.Class_hours_per_week = classHoursperweek;
            studyHours.Date_Studied = dateStudied;
            studyHours.Number_of_weeks = numberOfweeks;

            logic.AddStudyHours(studyHours);

            
        }


    }
}
