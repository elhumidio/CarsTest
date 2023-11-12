using CarPaymentPlanning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPaymentPlanning
{
    public class ReturnPlanning
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="financiation"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<MonthlyPayment> ReturnPlanningList(Vehicle vehicle)
        {
            decimal remainingDebt = vehicle.price - vehicle.finantiation.deposit;
            List<MonthlyPayment> lst = new List<MonthlyPayment>();
            DateTime actual = vehicle.deliveryDate;
            int month = vehicle.deliveryDate.AddMonths(1).Month;
            int year = actual.Year;
            int cont = 0;
            for (int  mth = month; mth <= (vehicle.finantiation.financePeriod + 12 - month) 
                && lst.Count < vehicle.finantiation.financePeriod; mth ++)
            {
                
                if (mth == 13)
                {
                    mth = 1;
                    year = year + 1;
                }
                     
                DateTime dt = new DateTime(year,mth,1);
                while (dt.DayOfWeek != DayOfWeek.Monday)
                {
                    dt = dt.AddDays(1);
                }
                MonthlyPayment payment = new MonthlyPayment();
                if (cont == 0)
                    payment.monthPayment = CalculateFirstPayment(vehicle);
                else if (cont > 0 && cont == (vehicle.finantiation.financePeriod - 1))
                    payment.monthPayment = CalculateLastPayment(vehicle);
                else
                    payment.monthPayment = CalculateRawMonthlyPayment(vehicle);
                payment.paymentDate = dt;
                remainingDebt = remainingDebt - CalculateRawMonthlyPayment(vehicle);
                if (remainingDebt > 0)
                    payment.DueAmount = remainingDebt;
                else payment.DueAmount = 0;
                lst.Add(payment);
                cont++;
                    }
            return lst;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public decimal CalculateRawMonthlyPayment(Vehicle vehicle)
        {
            decimal rawPayment = 0M;
            if(vehicle.finantiation.financePeriod > 0)
                 rawPayment = vehicle.price - vehicle.finantiation.deposit;
            var monthlyRawPayment = rawPayment / vehicle.finantiation.financePeriod;
            return monthlyRawPayment;
        }

        public decimal CalculateFirstPayment(Vehicle vehicle)
        {
            decimal ans = 0M;
            ans = CalculateRawMonthlyPayment(vehicle) + vehicle.finantiation.arrangementFee; 
            return ans;
        }

        public decimal CalculateLastPayment(Vehicle vehicle)
        {
            decimal ans = 0M;
            ans = CalculateRawMonthlyPayment(vehicle) + vehicle.finantiation.completionFee;
            return ans; 
        }


    }
}
