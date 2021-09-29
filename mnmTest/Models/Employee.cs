using System;
using System.Collections.Generic;
using System.Text;

namespace mnmTest.Models
{
    public class Employee
    {
        public int Id { set; get; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int  Age { get; set; }
        public int PositionId { set; get; }
        
    }
}
