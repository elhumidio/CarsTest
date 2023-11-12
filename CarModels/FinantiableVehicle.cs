using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace CarModels
{
    public class Finantiation : IFinantiation
    {
        [DepositAtLeast15Percent]
        [Required(ErrorMessage = "Deposit is mandatory")]
        public decimal deposit { get; set; }
        [Required(ErrorMessage = "Price is Mandatory")]
        public decimal price { get; set; }

        [Required]
        public decimal arrangementFee { get; set; }
        [Required]
        public int financePeriod { get; set; }
        [Required]
        public decimal completionFee { get; set; }
        public List<SelectListItem> FinanciationPossiblePeriods { get; set; }
        public List<MonthlyPayment> PaymentPlanningList { get; set; }
        public decimal monthlyGrossPayment { get; set; }


    }

    public class DepositAtLeast15Percent : ValidationAttribute
    {
        public DepositAtLeast15Percent()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vehiclePrice = validationContext.ObjectInstance.GetType().GetProperty("price");
            var vehicleDeposit = validationContext.ObjectInstance.GetType().GetProperty("deposit");
            var price = Convert.ToDecimal(vehiclePrice.GetValue(validationContext.ObjectInstance, null).ToString());
            var deposit = Convert.ToDecimal(vehicleDeposit.GetValue(validationContext.ObjectInstance, null).ToString());

            if (deposit < ((price * 15) / 100))
            {
                return new ValidationResult("Deposit must be at least 15% of the price");
                
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = "Deposit must be at least 15% of the price";
            rule.ValidationParameters.Add("price", "Price");
            rule.ValidationParameters.Add("deposit", "Deposit");
            rule.ValidationType = "depositIsEnough";

            yield return rule;
        }

    }
    }
