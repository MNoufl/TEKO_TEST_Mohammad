using mnmTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace mnmTest.Utalities
{
    public static class EmployeeGenerator
    {

        public static int minNameLength = 3; // Min length of the name
        public static int maxNameLength = 12; // Max length of the name

        public static int minAge = 18; // min age of the employee
        public static int maxAge = 60; // max age of the employee

        public static char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static Dictionary<int, string> jobs = Positions.Describtions;
        public static string GenerateName()
        {
            string result="";

            Random r = new Random();
            int nameLength = r.Next(minNameLength, maxNameLength);

            int chooseChar = r.Next(0, 26);

            result= alpha[chooseChar].ToString().ToUpper();


            for (int i=1;i<nameLength;i++)
            {
                chooseChar = r.Next(0, 26);
                result += alpha[chooseChar].ToString();
            }

            return result;
        }
        public static int GenerateJobId()
        {
            return new Random().Next(1, 10);
        }
        public static int GenerateAge()
        {
            return new Random().Next(minAge, maxAge);
        }



        public static List<Vacation> GenerateEmployeeVacations(int id)
        {
            List<int> periods = new List<int> { 14, 7, 7 };

            periods = periods.OrderBy(i => Guid.NewGuid()).ToList();

            Vacation vac1 = VacationGenerator.CreateVacation(id, periods[0]);
            Vacation vac2 = VacationGenerator.CreateVacation(id, periods[1]);
            if(VacationGenerator.CheckOverlapping(vac2,vac1))
            {
                if(vac1.EndDate.AddDays(vac2.Period+1).Year<VacationGenerator.year)
                {
                    vac2.StartDate = vac1.EndDate.AddDays(1);
                    vac2.EndDate = vac2.StartDate.AddDays(vac2.Period-1);
                }
                else
                {
                    vac2.StartDate = vac1.StartDate.AddDays(-vac2.Period-1);
                    vac2.EndDate = vac2.StartDate.AddDays(vac2.Period - 1);
                }
            }

            Vacation vac3 = VacationGenerator.CreateVacation(id, periods[2]);

            if (VacationGenerator.CheckOverlapping(vac3, vac2))
            {
                if (vac2.EndDate.AddDays(vac3.Period + 1).Year < VacationGenerator.year)
                {
                    vac3.StartDate = vac2.EndDate.AddDays(1);
                    vac3.EndDate = vac3.StartDate.AddDays(vac3.Period - 1);
                }
                else
                {
                    vac3.StartDate = vac2.StartDate.AddDays(-vac3.Period - 1);
                    vac3.EndDate = vac3.StartDate.AddDays(vac3.Period - 1);
                }
            }

            return new List<Vacation> { vac1,vac2,vac3};
        }

        public static Employee GenerateEmployee(int id)
        {
            Employee e = new Employee()
            {
                Id = id,
                FirstName= GenerateName(),
                SecondName= GenerateName(),
                LastName= GenerateName(),
                Age=GenerateAge(),
                PositionId=GenerateJobId(),
            };

            return e;
        }


        public static List<Employee> GenerateEmployeeList(int len)
        {

            List<Employee> res = new List<Employee>();

            for(int i=0;i<len;i++)
            {
                res.Add(GenerateEmployee(i));
            }

            return res;

        }


        public static List<Vacation> GenerateAllEmployeeVacations(int len)
        {
            List<Vacation> res = new List<Vacation>();


            for(int i=0;i<len;i++)
            {
                List<Vacation> temp = GenerateEmployeeVacations(i);

                res.Add(temp[0]);
                res.Add(temp[1]);
                res.Add(temp[2]);
            }

            return res;

        }

    }
}
