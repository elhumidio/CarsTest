using BusinessLogic;
using CarModels;
using NUnit.Framework;
using System.Collections.Generic;
using Telerik.JustMock;

namespace Tests
{
    public class CarPaymentTest
    {
        
        [SetUp]
        public void Setup()
        {
            
        }
/* [Test]
        public void CalculateFirstPaymentTest()
        {
            var planning = Mock.Create<IPlanning>();
            //IPlanning planning = new Planning();
            Vehicle v = new Vehicle();
            v.deliveryDate = new System.DateTime(2020,12,01);
            v.finantiation = new Finantiation();
            v.finantiation.PaymentPlanningList = new System.Collections.Generic.List<MonthlyPayment>();
            v.finantiation.arrangementFee = 50;
            v.finantiation.completionFee = 88;
            v.finantiation.deposit = 1000;
            v.finantiation.price = 15000;
            v.finantiation.financePeriod = 24;

            var isOk = false;
            var list = new List<MonthlyPayment>();
            Mock.Arrange(() => planning.CalculateFirstPayment(v)).DoInstead(()=> isOk = true);
            Mock.Arrange(() => planning.ReturnPlanningList(v)).DoInstead(()=> list = null);

            var result = planning.CalculateFirstPayment(v);
            Assert.IsNotNull(list);
            Assert.IsNotNull(result);
            Assert.IsTrue(isOk);
            Assert.AreNotEqual(0, result);
            Assert.Pass();
        }*/


        [Test]
        public void ReturnPlanningListTest()
        { }
    }
}