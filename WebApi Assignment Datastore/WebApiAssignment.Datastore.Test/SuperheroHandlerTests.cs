using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WebApiAssignment.Datastore.DataModels;

namespace WebApiAssignment.Datastore.Test
{
    [TestClass]
    public class SuperHeroHandlerTests
    {
        private int _lastUniverseId = -1;
        private int _lastSuperheroId = -1;

        [TestCleanup]
        public void Cleanup()
        {
            Datastore.UniverseStore.DeleteMany(x => true);
            Datastore.SuperheroStore.DeleteMany(x => true);
        }

        [TestInitialize]
        public void Initialize()
        {
            var universe = new Universe() { ParentCompany = "Disney", UniverseName = "Marvel" };
            Datastore.UniverseStore.InsertOne(universe);
            _lastUniverseId = universe.Id;

            var superhero = new Superhero() {
                YearOfAppearance = 2007,
                Abilities = (new[] { "Flight", "Strength" }).ToList(),
                AgeAtOrigin = 20,
                Alien = false,
                SuperheroName = "Iron Man",
                OriginStory = "Self-made",
                SecretIdentity = "Tony Stark",
                UniverseId = _lastUniverseId
            };
            Datastore.SuperheroStore.InsertOne(superhero);
            _lastSuperheroId = superhero.Id;
        }

        [TestMethod]
        public void GetSuperheroes_ReturnsItems()
        {
            var count = SuperheroHandler.GetSuperheroes().Count;
            Assert.AreNotEqual(0, count);
        }

        [TestMethod]
        public void GetSuperhero_ReturnsASuperhero()
        {
            var superhero = SuperheroHandler.GetSuperhero(_lastSuperheroId);
            Assert.AreEqual("Iron Man", superhero.SuperheroName);
        }

        [TestMethod]
        public void InsertSuperhero_WithMatchingUniverse_UpdatesId()
        {
            var superhero = new Superhero()
            {
                Id = -1,
                SuperheroName = "Captain America",
                UniverseId = _lastSuperheroId
            };
            SuperheroHandler.AddSuperhero(superhero);
            Assert.AreNotEqual(-1, superhero.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void InsertSuperhero_WithoutMatchingUniverse_Throws()
        {
            var superhero = new Superhero()
            {
                Id = -1,
                SuperheroName = "Captain America",
                UniverseId = _lastUniverseId + 5000
            };
            SuperheroHandler.AddSuperhero(superhero);
        }

        [TestMethod]
        public void UpdateSuperhero_WithMatchingUniverse_Updates()
        {
            var initialSuperhero = SuperheroHandler.GetSuperhero(_lastSuperheroId);
            initialSuperhero.SuperheroName = "Iron Mann";
            SuperheroHandler.UpdateSuperhero(initialSuperhero);
            var newSuperhero = SuperheroHandler.GetSuperhero(_lastSuperheroId);
            Assert.AreEqual("Iron Mann", newSuperhero.SuperheroName);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void UpdateSuperhero_WithoutMatchingUniverse_Throws()
        {
            var initialSuperhero = SuperheroHandler.GetSuperhero(_lastSuperheroId);
            initialSuperhero.UniverseId = _lastUniverseId + 900;
            SuperheroHandler.UpdateSuperhero(initialSuperhero);
        }
    }
}
