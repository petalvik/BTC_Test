using DAL.Models;
using DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BTC.Tests.Controllers
{
    [TestClass]
    public class HeroRepositoryTest
    {
        [TestMethod]
        public void CreateDeleteTest()
        {
            var heroes = new List<Hero> { new Hero(), new Hero() };

            Mock<IRepository<Hero>> mock = new Mock<IRepository<Hero>>();

            mock.Setup(m => m.Get(null, null, "")).Returns(heroes);

            mock.Setup(m => m.Insert(It.IsAny<Hero>())).Callback<Hero>(h => heroes.Add(h));

            mock.Setup(m => m.Delete(It.IsAny<Hero>())).Callback<Hero>(h => heroes.Remove(h));


            var repo = mock.Object;
            Assert.IsNotNull(repo);
            Assert.IsTrue(repo.Get().Count() == 2);

            var hero = new Hero();

            repo.Insert(hero);
            Assert.IsTrue(repo.Get().Count() == 3);
            mock.Verify(m => m.Insert(It.IsAny<Hero>()), Times.Once());

            repo.Delete(hero);
            Assert.IsTrue(repo.Get().Count() == 2);
            mock.Verify(m => m.Delete(It.IsAny<Hero>()), Times.Once());
        }
    }
}