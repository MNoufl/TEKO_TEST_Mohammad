using System;
using System.Collections.Generic;
using System.Text;

namespace mnmTest.Models
{
    public class Vacation
    {

        public Vacation( int?  employeeId,DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.EmployeeId = employeeId;
            Period = (endDate - startDate).Days+1;
        }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Period { set; get; }

        public int? EmployeeId { set; get; }
    }
}
