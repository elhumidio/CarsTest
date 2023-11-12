using System.Collections.Generic;
using CarModels;

namespace BusinessLogic
{
    public interface IPlanning
    {
        decimal CalculateFirstPayment(Vehicle vehicle);
        decimal CalculateLastPayment(Vehicle vehicle);
        decimal CalculateGrossMonthlyPayment(Vehicle vehicle);
        List<MonthlyPayment> ReturnPlanningList(Vehicle vehicle);
    }
}