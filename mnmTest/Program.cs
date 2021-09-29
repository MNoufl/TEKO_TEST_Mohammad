using System;
using System.Collections.Generic;
using mnmTest.Models;
using mnmTest.Utalities;
using System.Linq;
using System.IO;

namespace mnmTest
{
    class Program
    {
        static void Main(string[] args)
        {

            int len = 100;

            List<Employee> employees = EmployeeGenerator.GenerateEmployeeList(len);

            List<Vacation> vacations = EmployeeGenerator.GenerateAllEmployeeVacations(len);


            DateTime startDate;
            DateTime endDate;

            int year = DateTime.Now.Year;
            int month=1;
            int day=1;
            int daysOfMonth;

            #region start Date

            Console.Write("Enter the start date of the vacation (year is always "+ year + ")\n");
        
        startMonth:
            Console.Write("1) Enter the month (1-12): \n");
            try
            {
                month = int.Parse(Console.ReadLine());
                
                if(month>12 || month<1)
                {
                    Console.Write("Incorrect month input \n");
                    Console.Write("\n");
                    goto startMonth;
                }
            }
            catch
            {
                Console.Write("Incorrect month input \n");
                Console.Write("\n");
                goto startMonth;
            }
            daysOfMonth = DateTime.DaysInMonth(year, month);

            startDay:
            Console.Write("2) Enter the day (1-"+ daysOfMonth + "): \n");
            try
            {
                day = int.Parse(Console.ReadLine());

                if (day > daysOfMonth || day < 1)
                {
                    Console.Write("Incorrect day input \n");
                    Console.Write("\n");
                    goto startDay;
                }
            }
            catch
            {
                Console.Write("Incorrect day input \n");
                Console.Write("\n");
                goto startDay;
            }
            startDate = new DateTime(year, month, day);

            Console.WriteLine("Start Date is: " + day + "." + month + "." + year);

        #endregion


            #region End Date
        inputEndDate:
            Console.Write("Enter the end date of the vacation (year is always " + year + ")\n");

        endMonth:
            Console.Write("1) Enter the month (1-12): \n");
            try
            {
                month = int.Parse(Console.ReadLine());

                if (month > 12 || month < 1)
                {
                    Console.Write("Incorrect month input \n");
                    Console.Write("\n");
                    goto endMonth;
                }
            }
            catch
            {
                Console.Write("Incorrect month input \n");
                Console.Write("\n");
                goto endMonth;
            }
            daysOfMonth = DateTime.DaysInMonth(year, month);

        endDay:
            Console.Write("2) Enter the day (1-" + daysOfMonth + "): \n");
            try
            {
                day = int.Parse(Console.ReadLine());

                if (day > daysOfMonth || day < 1)
                {
                    Console.Write("Incorrect day input \n");
                    Console.Write("\n");
                    goto endDay;
                }
            }
            catch
            {
                Console.Write("Incorrect day input \n");
                Console.Write("\n");
                goto endDay;
            }

            endDate = new DateTime(year, month, day);


            Console.WriteLine("End Date is: " + day + "." + month + "." + year);


            if ((endDate-startDate).Days+1>14)
            {
                Console.Write("Vacation cannot be longer than 14 days enter end date again !!\n");
                Console.Write("\n");
                goto inputEndDate;
            }

            if ((endDate - startDate).Days + 1 <= 0)
            {
                Console.Write("End date must be after start date !!\n");
                Console.Write("\n");
                goto inputEndDate;
            }
            #endregion



            List<Vacation> crossVacations = (from x in vacations
                                             where (startDate >= x.StartDate && startDate <= x.EndDate)
                                             ||
                                             (endDate >= x.StartDate && endDate <= x.EndDate)
                                             select x).ToList();

            List<Employee> crossVacationsEmployees =
                (from e in employees where crossVacations.Select(a=>a.EmployeeId).Contains(e.Id) select e).ToList();


            var result = (from vacs in crossVacations
                        join emps in crossVacationsEmployees
                             on vacs.EmployeeId equals emps.Id
                        select new
                        {
                            vacs.StartDate,
                            vacs.EndDate,
                            emps.FirstName,
                            emps.SecondName,
                            emps.LastName,
                            emps.Age
                        }).ToList();


            var employess_under_30 = result.Where(a => a.Age <= 30).ToList();
            var employess_30_50 = result.Where(a => a.Age <= 50 && a.Age>30).ToList();
            var employess_over_50 = result.Where(a =>  a.Age>50).ToList();

            List<Vacation> noCrossVacations = (from x in vacations
                                               where (startDate < x.StartDate && endDate < x.StartDate)
                                               ||
                                               (startDate > x.EndDate && endDate > x.EndDate)
                                               select x).ToList();
            List<Employee> noCrossVacationsEmployees =
    (from e in employees where noCrossVacations.Select(a => a.EmployeeId).Contains(e.Id) select e).ToList();


             var noCrossVac = (from vacs in noCrossVacations
                               join emps in noCrossVacationsEmployees
                               on vacs.EmployeeId equals emps.Id
                          select new
                          {
                              vacs.StartDate,
                              vacs.EndDate,
                              emps.FirstName,
                              emps.SecondName,
                              emps.LastName,
                              emps.Age
                          }).ToList();

            using (StreamWriter writetext = new StreamWriter("Results.txt"))
            {
                writetext.WriteLine("Пересечение отпуска с сотрудниками, моложе 30:");
                foreach(var v in employess_under_30)
                {
                    writetext.WriteLine("(" + v.StartDate.ToString("dd.MM.yyyy") + " - " + v.EndDate.ToString("dd.MM.yyyy") + " - " +
                        v.FirstName + " " + v.SecondName + " " + v.LastName + " - " +
                        v.Age + ")"
                        );
                }
                writetext.WriteLine("\n");

                writetext.WriteLine("Пересечение отпуска с сотрудниками, старше 30 моложе 50:");
                foreach (var v in employess_30_50)
                {
                    writetext.WriteLine("("+v.StartDate.ToString("dd.MM.yyyy") + " - " + v.EndDate.ToString("dd.MM.yyyy") + " - " +
                        v.FirstName + " " + v.SecondName + " " + v.LastName + " - " +
                        v.Age+")"
                        );
                }
                writetext.WriteLine("\n");

                writetext.WriteLine("Пересечение отпуска с сотрудниками, старше 50:");
                foreach (var v in employess_over_50)
                {
                    writetext.WriteLine("(" + v.StartDate.ToString("dd.MM.yyyy") + " - " + v.EndDate.ToString("dd.MM.yyyy") + " - " +
                        v.FirstName + " " + v.SecondName + " " + v.LastName + " - " +
                        v.Age + ")"
                        );
                }
                writetext.WriteLine("\n");


                writetext.WriteLine("Отпуска без пересечения:");
                foreach (var v in noCrossVac)
                {
                    writetext.WriteLine("(" + v.StartDate.ToString("dd.MM.yyyy") + " - " + v.EndDate.ToString("dd.MM.yyyy") + " - " +
                        v.FirstName + " " + v.SecondName + " " + v.LastName + " - " +
                        v.Age + ")"
                        );
                }
            }

            int x1 = 0;
            x1++;

        }
    }
}
