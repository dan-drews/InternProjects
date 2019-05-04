using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiAssignment.Datastore.DataModels;

namespace WebApiAssignment.Datastore.Test
{
    [TestClass]
    public class UniverseHandlerTests
    {

        private int _lastUniverseId = -1;

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
        }

        [TestMethod]
        public void GetUniverses_ReturnsItems()
        {
            var universes = UniverseHandler.GetUniverses();
            Assert.AreNotEqual(0, universes.Count);
        }

        [TestMethod]
        public void GetUniverse_ReturnsAUniverse()
        {
            var universe = UniverseHandler.GetUniverse(_lastUniverseId);
            Assert.AreEqual("Marvel", universe.UniverseName);
        }

        [TestMethod]
        public void InsertUniverse_UpdatesId()
        {
            var universe = new Universe() { UniverseName = "DC", ParentCompany = "WB", Id = -1 };
            UniverseHandler.AddUniverse(universe);
            Assert.AreNotEqual(-1, universe.Id);
        }

        [TestMethod]
        public void UpdateUniverse_UpdatesSuccessfully()
        {
            var universe = UniverseHandler.GetUniverse(_lastUniverseId);
            universe.ParentCompany = "Omaha";
            UniverseHandler.UpdateUniverse(universe);
            var updatedUniverse = UniverseHandler.GetUniverse(_lastUniverseId);
            Assert.AreEqual("Omaha", updatedUniverse.ParentCompany);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void DeleteUniverse_UsedUniverse_ThrowsException()
        {
            var sh = new Superhero() { UniverseId = _lastUniverseId };
            sh.SuperheroName = "Captain America";
            SuperheroHandler.AddSuperhero(sh);
            UniverseHandler.DeleteUniverse(_lastUniverseId);
        }

        [TestMethod]
        public void DeleteUniverse_Unused_DeletesSuccessfully()
        {
            var initialCount = UniverseHandler.GetUniverses().Count;
            UniverseHandler.DeleteUniverse(_lastUniverseId);
            var newCount = UniverseHandler.GetUniverses().Count;
            Assert.AreEqual(initialCount - 1, newCount);
        }
    }
}
