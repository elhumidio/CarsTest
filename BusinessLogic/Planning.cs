using CarModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BusinessLogic
{
    public class Planning : IPlanning
    {
        public static Logger log = LogManager.GetCurrentClassLogger();
        public Planning()
        { }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="financiation"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<MonthlyPayment> ReturnPlanningList(Vehicle vehicle)
        {
            log.Info("Starting Method",MethodBase.GetCurrentMethod());
            try {
                decimal remainingDebt = vehicle.finantiation.price - vehicle.finantiation.deposit;
                List<MonthlyPayment> lst = new List<MonthlyPayment>();
                DateTime actual = vehicle.deliveryDate;
                int month = vehicle.deliveryDate.AddMonths(1).Month;
                int year = actual.Year;
                int cont = 0;
                

                for (int mth = month; mth <= (vehicle.finantiation.financePeriod + 12 - month)
                    && lst.Count < vehicle.finantiation.financePeriod; mth++)
                {

                    if (mth == 13)
                    {
                        mth = 1;
                        year = year + 1;
                    }

                    DateTime dt = new DateTime(year, mth, 1);
                    while (dt.DayOfWeek != DayOfWeek.Monday)
                    {
                        dt = dt.AddDays(1);
                    }
                    MonthlyPayment payment = new MonthlyPayment();
                    vehicle.finantiation.monthlyGrossPayment = CalculateGrossMonthlyPayment(vehicle);
                    remainingDebt = CalculatesMonthlyPayment(vehicle, remainingDebt, cont, dt, payment);
                    
                    lst.Add(payment);
                    cont++;
                }
                return lst;
            }
            catch (Exception ex) {
                //return ex; log this
                log.Error(ex.Message);
                return new List<MonthlyPayment>();
            }
          

        }


        /// <summary>
        /// CAlculates monthly payment
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="remainingDebt"></param>
        /// <param name="cont"></param>
        /// <param name="dt"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        private decimal CalculatesMonthlyPayment(Vehicle vehicle, decimal remainingDebt, int cont, DateTime dt, MonthlyPayment payment)
        {

            log.Info("Starting Method", MethodBase.GetCurrentMethod());
            try {
                if (cont == 0)
                    payment.monthPayment = CalculateFirstPayment(vehicle);
                else if (cont > 0 && cont == (vehicle.finantiation.financePeriod - 1))
                    payment.monthPayment = CalculateLastPayment(vehicle);
                else
                    payment.monthPayment = vehicle.finantiation.monthlyGrossPayment;

                payment.paymentDate = dt.ToShortDateString();
                remainingDebt = remainingDebt - payment.monthPayment;

                if (remainingDebt > 0)
                    payment.DueAmount = remainingDebt;
                else payment.DueAmount = 0;


                return remainingDebt;
            }
            catch (Exception ex) {

                log.Error("Error en method", ex + " - " + MethodBase.GetCurrentMethod());
                return -1M;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public decimal CalculateGrossMonthlyPayment(Vehicle vehicle)
        {
            log.Info("Starting Method", MethodBase.GetCurrentMethod());
            try {
                decimal rawPayment = 0M;
                if (vehicle.finantiation.financePeriod > 0)
                    rawPayment = vehicle.finantiation.price - vehicle.finantiation.deposit;
                var monthlyRawPayment = rawPayment / vehicle.finantiation.financePeriod;
                return monthlyRawPayment;
            }
            catch (Exception ex) {
                log.Error("Error en method", ex + " - " + MethodBase.GetCurrentMethod());
                return -1;
            }

            
        }

        public decimal CalculateFirstPayment(Vehicle vehicle)
        {
            decimal ans = 0M;
            ans = CalculateGrossMonthlyPayment(vehicle) + vehicle.finantiation.arrangementFee;
            return ans;
        }

        public decimal CalculateLastPayment(Vehicle vehicle)
        {
            decimal ans = 0M;
            ans = CalculateGrossMonthlyPayment(vehicle) + vehicle.finantiation.completionFee;
            return ans;
        }
    }
}
