using ClassLibraryTask1;
using PROG6212POE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace PROG6212POE.Controllers
{
    public class HomeController : Controller
    {
        
    
        public ActionResult Index()
        {
            return View();
        }

   

        // REGISTER
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection Registerform)
        {
            string username = Registerform["User_Name"] == "" ? null : Registerform["User_Name"];
            string email = Registerform["User_Email"] == "" ? null : Registerform["User_Email"];
            string password = Registerform["User_Password"] == "" ? null : Registerform["User_Password"]; ;
 


            string converter = LoginInfo.MD5Hash(password); 

            RegisterInfo.register(username, email, converter);
            LoginInfo.currentuser = username;
            MessageBox.Show("YOU HAVE SUCESSFULLY REGISTERED", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);

            return RedirectToAction("Login");
        }

        // LOGIN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection loginform)
        {
            string username = loginform["User_Name"]==""?null:loginform["User_Name"];
            string password = loginform["User_Password"] == "" ? null : loginform["User_Password"]; ;
         

            if (LoginInfo.login(username, password))
            {
                LoginInfo.currentuser = username;
                // update days list
                LogicDAL logic = new LogicDAL();
                List<SummaryInfo> moduleList = (List<SummaryInfo>)logic.GetRegisterStudentHours(username);
                List<StudentModuleInfo> moduleStudentList = new List<StudentModuleInfo>();

                foreach (var item in moduleList)
                {
                    moduleStudentList.Add(logic.GetStudentModuleByID(item.Code));
                }
                // days of week array
                string[] daysofweek = { "Monday", "Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday" };
                // for loop within foreach
                //  compare
                // check todays date compare to day.dayofweek
                DateTime userday = DateTime.Today;
                String todayValue = userday.DayOfWeek +"";
              

                StudentModuleInfo.AvailableDays_Of_Week.Clear();
                foreach(var day in moduleStudentList)
                {
                    if (todayValue.Equals(day.Day_Of_Week))
                    {
                        MessageBox.Show("YOU HAVE A MODULE SCHEDULED TO STUDY FOR TODAY -  " + day.Code , "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                      
                    }

                    Boolean found = false;

                    for (int i = 0; i < 7; i++)
                    {
                        if (day.Day_Of_Week.Equals(daysofweek[i]))
                        {
                            
                            found = true;
                            daysofweek[i] = "0";

                        }
                    }

                        if (!found)
                        {
                            StudentModuleInfo.AvailableDays_Of_Week.Add(day.Day_Of_Week);
                        }
                    
                }
      


                for (int i = 0; i < 7; i++)
                {
                    if (!daysofweek[i].Equals("0"))
                    {
                        StudentModuleInfo.AvailableDays_Of_Week.Add(daysofweek[i]);
                    }
                }

                return RedirectToAction("Module");
            }

            return RedirectToAction("Login");
        }

        // STUDENT MODULE
        public ActionResult Module()
        {
            // create student model info model
            StudentModuleInfo stm = new StudentModuleInfo();
            stm.Days_Of_Week = StudentModuleInfo.AvailableDays_Of_Week;
            // call set days of week
            //login info . current user
            return View(stm);
        }

        [HttpPost]
        public ActionResult Module(FormCollection moduleform)
        {
            LogicDAL logic = new LogicDAL();
            StudentModuleInfo.studentOutput = ((List<StudentModuleInfo>)logic.GetAllStudentModules());

            string Code = moduleform["Code"] == "" ? null : moduleform["Code"];
            string Module_Name = moduleform["Module_Name"] == "" ? null : moduleform["Module_Name"];
            int Number_of_Credits = Convert.ToInt32(moduleform["Number_of_Credits"] == "" ? null : moduleform["Number_of_Credits"]);
            int Class_hours_per_week = Convert.ToInt32(moduleform["Class_hours_per_week"] == "" ? null : moduleform["Class_hours_per_week"]);
            int Number_of_weeks = Convert.ToInt32(moduleform["Number_of_weeks"] == "" ? null : moduleform["Number_of_weeks"]);
            string start_date = moduleform["start_date"] == "" ? null : moduleform["start_date"];
            string day_of_week = moduleform["SelectedValue"] == "" ? null : moduleform["SelectedValue"];

            if (!day_of_week.Equals("SelectedValue"))
            {
                StudentModuleInfo.studentmodule(Code, Module_Name, Number_of_Credits, Class_hours_per_week, Number_of_weeks, start_date, day_of_week);
                return RedirectToAction("StudyHours");
            }

            else
            {
                MessageBox.Show("PLEASE SELECT A VALID DAY", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
             
                return RedirectToAction("Module");

            }
       

          
        }

        // STUDY HOURS
        public ActionResult StudyHours()
        {
            return View(new StudyHoursInfo());
        }

        [HttpPost]
        public ActionResult StudyHours(FormCollection studyhoursform)
        {
            int Number_of_Credits = Convert.ToInt32(studyhoursform["Number_of_Credits"] == "" ? null : studyhoursform["Number_of_Credits"]);
            int Class_hours_per_week = Convert.ToInt32(studyhoursform["Class_hours_per_week"] == "" ? null : studyhoursform["Class_hours_per_week"]);
            int Number_of_weeks = Convert.ToInt32(studyhoursform["Number_of_weeks"] == "" ? null : studyhoursform["Number_of_weeks"]);

            int numcred, numweek, numhr;
            numcred = Convert.ToInt32(Number_of_Credits);
            numweek = Convert.ToInt32(Number_of_weeks);
            numhr = Convert.ToInt32(Class_hours_per_week);

            double studyhours = (numcred * 10 / numweek) - numhr;


            return RedirectToAction("Summary");
        }

        //calusate method
        public double calculate(int numcred, int numweek, int numhr)
        {

            return (numcred * 10 / numweek) - numhr;

        }

        // SUMMARY
        public ActionResult Summary()
        {

            LogicDAL logic = new LogicDAL();
            List<SummaryInfo> moduleList = new List<SummaryInfo>();
            moduleList = logic.GetRegisterStudentHours(LoginInfo.currentuser).ToList();
            List<StudentModuleInfo> studentmoduleList = new List<StudentModuleInfo>();
         

            foreach (SummaryInfo useroutput in moduleList)
            {

                StudentModuleInfo stm = new StudentModuleInfo();
             
                stm = logic.GetStudentModuleByID(useroutput.Code);
                stm.SelfStudyHours = calculate(stm.Number_of_Credits, 50, stm.Class_hours_per_week);
                studentmoduleList.Add(stm);


            }

       
            return View(studentmoduleList);
     
        }
    }
}



