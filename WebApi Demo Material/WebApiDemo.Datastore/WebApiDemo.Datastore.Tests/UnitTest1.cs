using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApiDemo.Datastore.Tests
{
    [TestClass]
    public class ToDoHandlerTest
    {
        private int _latestToDoId = 0;

        [TestCleanup()]
        public void Cleanup()
        {
            ToDoHandler.WipeDB();
        }

        [TestInitialize]
        public void Initialize()
        {
            var td = new DataModels.ToDo { Title = "Blah!" };
            ToDoHandler.InsertToDo(td);
            _latestToDoId = td.Id;
        }


        [TestMethod]
        public void Insert()
        {
            var td = new DataModels.ToDo { Title = "abcd" };
            ToDoHandler.InsertToDo(td);
            Assert.AreNotEqual(0, td.Id);
        }

        [TestMethod]
        public void GetAll()
        {
            var length = ToDoHandler.GetToDos().Count;
            Assert.AreNotEqual(0, length);
        }

        [TestMethod]
        public void GetOne()
        {
            var td = ToDoHandler.GetToDo(_latestToDoId);
            Assert.IsNotNull(td);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void GetOneNotExists()
        {
            var td = ToDoHandler.GetToDo(_latestToDoId + 5000);
            Assert.IsNotNull(td);
        }

        [TestMethod]
        public void UpdateOneExists()
        {
            var td = ToDoHandler.GetToDo(_latestToDoId);
            td.Title = "Hi There!";
            ToDoHandler.UpdateToDo(td);
            var todoFromDb = ToDoHandler.GetToDo(_latestToDoId);
            Assert.AreEqual("Hi There!", todoFromDb.Title);
        }

    }
}
