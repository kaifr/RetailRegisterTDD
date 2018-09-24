using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RetailSystem.Tests
{
    public class CashRegisterTests

    {
        [TestCase(1, 60)]
        [TestCase(2, 120)]
        [TestCase(3, 120)]
        [TestCase(4, 180)]
        [TestCase(5, 240)]
        [TestCase(6, 240)]
        [TestCase(7, 300)]
        [Test]
        public void AddPairsOfRubberGloves_returnsKr(int number, int totalPrice)
        {
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU A", number);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(totalPrice));
        }


        [TestCase(1, 399)]
        [TestCase(2, 798)]
        [TestCase(3, 999)]
        [TestCase(4, 1398)]
        [Test]
        public void AddNumbersOfStethoscopes_returnsKr(int number, int totalPrice)
        {
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU B", number);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(totalPrice));
        }

    }
}
