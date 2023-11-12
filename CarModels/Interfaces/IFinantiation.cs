using System.Collections.Generic;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace CarModels
{
    public interface IFinantiation
    {
        decimal arrangementFee { get; set; }
        decimal completionFee { get; set; }
        decimal deposit { get; set; }
        int financePeriod { get; set; }
        List<SelectListItem> FinanciationPossiblePeriods { get; set; }
        List<MonthlyPayment> PaymentPlanningList { get; set; }
        decimal price { get; set; }
    }
}