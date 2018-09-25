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

        [Test]
        public void OneKiloOfTalcumPowder_returnsKr20()
        {
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU C", 1000);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(20));
        }

        [Test]
        public void AmountOfTalcumPowderCostLessOneOre_returnsKr0()
        {
            const double smallAmountCostLessThanOneOere = 1 / 1.954;
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU C", smallAmountCostLessThanOneOere);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(0));
        }

        [Test]
        public void AmountOfTalcumPowderALittleMoreThanOneOre_returnsKr0()
        {
            const double smallAmountCostLessThanOneOere = 1 / 1.953;
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU C", smallAmountCostLessThanOneOere);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(0));
        }

        [TestCase(1000, 20)]
        [TestCase(500, 10)]
        [TestCase(333, 7)]
        [Test]
        public void AmountOfTalcumPowder_returnsKr(int amount, int KrPrice)
        {
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU C", amount);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(KrPrice));
        }


        [Test]
        public void AddingOneOfEachDifferentProducts_ReturnsKr475()
        {
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU A", 1);
            cashRegister.Add("PLU B", 1);
            cashRegister.Add("PLU C", 1000);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(478));
        }

        [Test]
        public void AddingOneBundleOfEachDifferentProducts_ReturnsKr475()
        {
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU A", 3);
            cashRegister.Add("PLU B", 3);
            cashRegister.Add("PLU C", 1000);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(1138));
        }

        [Test]
        public void AddingSeveralDifferentProductsNotInOrder_ReturnsKr2030()
        {
            var cashRegister = new RetailPayment();
            cashRegister.Add("PLU C", 750);
            cashRegister.Add("PLU A", 1);
            cashRegister.Add("PLU B", 2);
            cashRegister.Add("PLU C", 1000);
            cashRegister.Add("PLU B", 1);
            cashRegister.Add("PLU A", 2);
            cashRegister.Add("PLU A", 1);
            cashRegister.Add("PLU B", 2);
            cashRegister.Add("PLU C", 1000);
            var totalCost = cashRegister.CalculateCost();
            Assert.That(totalCost, Is.EqualTo(2030));
        }
    }
}
