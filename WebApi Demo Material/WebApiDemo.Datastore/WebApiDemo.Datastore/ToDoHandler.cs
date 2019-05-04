using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiDemo.Datastore.DataModels;

namespace WebApiDemo.Datastore
{
    public static class ToDoHandler
    {
        private static DataStore _store;

        static ToDoHandler()
        {
            _store = new DataStore("data.json");
        }

        public static List<DataModels.ToDo> GetToDos()
        {
            return _store.GetCollection<DataModels.ToDo>().AsQueryable().ToList();
        }

        public static DataModels.ToDo GetToDo(int id)
        {
            return _store.GetCollection<DataModels.ToDo>().Find(x => x.Id == id).FirstOrDefault() ?? throw new Exception("Not Found");
        }

        public static void InsertToDo(ToDo todo)
        {
            _store.GetCollection<DataModels.ToDo>().InsertOne(todo);
        }

        public static void UpdateToDo(ToDo todo)
        {
            _store.GetCollection<DataModels.ToDo>().UpdateOne(todo.Id, todo);
        }

        public static void WipeDB()
        {
            _store.GetCollection<ToDo>().DeleteMany(x => true);
        }
    }
}
