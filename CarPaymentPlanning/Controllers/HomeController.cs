using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using CarModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarPaymentPlanning.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private IPlanning _planning;

        public HomeController(IConfiguration config, IPlanning planning)
        {
            _config = config;
            _planning = planning;
        }

        public IActionResult Results()
        {
            return View();
        }
            public IActionResult Index()
        {
            var down = _config.GetValue<string>("MySettings:downPayment");
            var completionFee = _config.GetValue<string>("MySettings:completionFee");
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Results(Vehicle model)
        {
            //TODO save values downpayment and completion fee in config
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index","Home");
            }
            DateTime dt = model.deliveryDate;
            model.finantiation.financePeriod = Convert.ToInt32(Request.Form["sctFinanceOpt"].ToString());
            
            //Planning rp = new Planning();
            var list = _planning.ReturnPlanningList(model);
            //model.finantiation.arrangementFee
            
            model.finantiation.PaymentPlanningList = list;
            ViewBag.price = model.finantiation.price;
            ViewBag.deposit = model.finantiation.deposit;
            ViewBag.arrangementFee = model.finantiation.arrangementFee;
            ViewBag.completionFee = model.finantiation.completionFee;
            ViewBag.deliveryDate = model.deliveryDate.ToShortDateString();
            ViewBag.payments = model.finantiation.financePeriod;
             return View(model.finantiation.PaymentPlanningList);
        }


        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
