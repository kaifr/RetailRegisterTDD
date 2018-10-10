using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using RetailSystem.DAL;


namespace RetailSystem.Tests
{
    public class CashRegisterTests
    {

        private IQueryable<Product> _productList;
        private Mock<DbSet<Product>> _mockSet;
        private Mock<ProductContext> _mockContext;

        [SetUp]
        public void SetupRepository()
        {
            _productList = new List<Product>
            {
                new Product
                {
                    Name = "PLU A",
                    Price = 59.90,
                    Unit = Unit.Pieces,
                    Discount = Discount.GetXPayForY,
                    GetXPayForY = new Tuple<int, int>(3, 2)
                },
                new Product
                {
                    Name = "PLU B",
                    Price = 399,
                    Unit = Unit.Pieces,
                    Discount = Discount.AmountIsABundle,
                    Bundle = 3,
                    BundlePrice = 999
                },
                new Product
                {
                    Name = "PLU C",
                    Price = 19.54 / 1000,
                    Unit = Unit.Amount,
                    Discount = Discount.None
                }
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Product>>();
            _mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(_productList.Provider);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(_productList.Expression);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(_productList.ElementType);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_productList.GetEnumerator());

            _mockContext = new Mock<ProductContext>();
        }
        
        [Test]
        public void GetAllProducts_by_name()
        {
            _mockContext.Setup(c => c.Products).Returns(_mockSet.Object);

            var repo = new ProductRepository(_mockContext.Object);
            var products = repo.GetAllProducts().ToList();

            Assert.AreEqual(3, products.Count);
            Assert.AreEqual("PLU A", products[0].Name);
            Assert.AreEqual("PLU B", products[1].Name);
            Assert.AreEqual("PLU C", products[2].Name);
        }

        
        

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
            _mockContext.Setup(c => c.Products).Returns(_mockSet.Object);

            var repo = new ProductRepository(_mockContext.Object);
            var cashRegister = new RetailPayment(repo);
            cashRegister.Add("PLU A", number);
            var totalCost = cashRegister.CalculateCost(cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(totalPrice));
        }

        
        [TestCase(1, 399)]
        [TestCase(2, 798)]
        [TestCase(3, 999)]
        [TestCase(4, 1398)]
        [Test]
        public void AddNumbersOfStethoscopes_returnsKr(int number, int totalPrice)
        {
            _mockContext.Setup(c => c.Products).Returns(_mockSet.Object);

            var repo = new ProductRepository(_mockContext.Object);
            var cashRegister = new RetailPayment(repo);
            cashRegister.Add("PLU B", number);
            var totalCost = cashRegister.CalculateCost(cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(totalPrice));
        }

        /*
        [Test]
        public void OneKiloOfTalcumPowder_returnsKr20()
        {
            _cashRegister.Add("PLU C", 1000);
            var totalCost = _cashRegister.CalculateCost(_cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(20));
        }

        [Test]
        public void AmountOfTalcumPowderCostLessOneOre_returnsKr0()
        {
            const double smallAmountCostLessThanOneOere = 1 / 1.954;
            _cashRegister.Add("PLU C", smallAmountCostLessThanOneOere);
            var totalCost = _cashRegister.CalculateCost(_cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(0));
        }

        [Test]
        public void AmountOfTalcumPowderALittleMoreThanOneOre_returnsKr0()
        {
            const double smallAmountCostLessThanOneOere = 1 / 1.953;
            _cashRegister.Add("PLU C", smallAmountCostLessThanOneOere);
            var totalCost = _cashRegister.CalculateCost(_cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(0));
        }

        [TestCase(1000, 20)]
        [TestCase(500, 10)]
        [TestCase(333, 7)]
        [Test]
        public void AmountOfTalcumPowder_returnsKr(int amount, int KrPrice)
        {
            _cashRegister.Add("PLU C", amount);
            var totalCost = _cashRegister.CalculateCost(_cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(KrPrice));
        }


        [Test]
        public void AddingOneOfEachDifferentProducts_ReturnsKr475()
        {
            _cashRegister.Add("PLU A", 1);
            _cashRegister.Add("PLU B", 1);
            _cashRegister.Add("PLU C", 1000);
            var totalCost = _cashRegister.CalculateCost(_cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(478));
        }

        [Test]
        public void AddingOneBundleOfEachDifferentProducts_ReturnsKr475()
        {
            _cashRegister.Add("PLU A", 3);
            _cashRegister.Add("PLU B", 3);
            _cashRegister.Add("PLU C", 1000);
            var totalCost = _cashRegister.CalculateCost(_cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(1138));
        }

        [Test]
        public void AddingSeveralDifferentProductsNotInOrder_ReturnsKr2030()
        {
            _cashRegister.Add("PLU C", 750);
            _cashRegister.Add("PLU A", 1);
            _cashRegister.Add("PLU B", 2);
            _cashRegister.Add("PLU C", 1000);
            _cashRegister.Add("PLU B", 1);
            _cashRegister.Add("PLU A", 2);
            _cashRegister.Add("PLU A", 1);
            _cashRegister.Add("PLU B", 2);
            _cashRegister.Add("PLU C", 1000);
            var totalCost = _cashRegister.CalculateCost(_cashRegister.GetItems());
            Assert.That(totalCost, Is.EqualTo(2030));
        } */
    }
}
