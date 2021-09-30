using System;
using System.Collections.Generic;
using System.Text;
using mnmTest.Models;

namespace mnmTest.Utalities
{
    public static class VacationGenerator
    {

        public static int year = DateTime.Now.Year;


        public static bool CheckOverlapping(Vacation vac1,Vacation vac2)
        {

            return ((vac2.StartDate >= vac1.StartDate && vac2.StartDate <= vac1.EndDate)
                || (vac2.EndDate >= vac1.StartDate && vac2.EndDate <= vac1.EndDate));
        }

        public static Vacation CreateVacation(int? emmployeeId,int period)
        {

            int startDate_Month = new Random().Next(1, 12);
            int startDate_Day = new Random().Next(1, DateTime.DaysInMonth(year,startDate_Month));

            DateTime startDate = new DateTime(year, startDate_Month, startDate_Day);

            DateTime endDate = startDate.AddDays(period-1);

           if (endDate.Year>year)
            {
                startDate= startDate.AddDays(-period-1);
                endDate = startDate.AddDays(period-1);
            }

            Vacation res = new Vacation(emmployeeId,startDate, endDate);

            return res;
        }


    }
}
