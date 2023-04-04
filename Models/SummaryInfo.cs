using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6212POE.Models
{
   public  class SummaryInfo
    {
        public string Code { get; set; }
        [Required]
        public string User_Name { get; set; }


        //RETURNING DATA FROM GET REGISTER STUDENT HOURS STORED PROCEDURE
        public IEnumerable<SummaryInfo> summary()
        {
            LogicDAL logic = new LogicDAL();
            return logic.GetRegisterStudentHours(LoginInfo.currentuser);
        }
    }
}
