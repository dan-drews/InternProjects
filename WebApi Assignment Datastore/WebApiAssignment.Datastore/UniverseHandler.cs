using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiAssignment.Datastore.DataModels;

namespace WebApiAssignment.Datastore
{
    public static class UniverseHandler
    {
        public static List<Universe> GetUniverses() => Datastore.UniverseStore.AsQueryable().ToList();

        public static Universe GetUniverse(int id) => Datastore.UniverseStore.Find(x => x.Id == id).FirstOrDefault() ?? throw new Exception($"No universe found with ID: {id}");

        public static void AddUniverse(Universe universe) => Datastore.UniverseStore.InsertOne(universe);

        public static void UpdateUniverse(Universe universe) => Datastore.UniverseStore.UpdateOne(universe.Id, universe);

        public static void DeleteUniverse(int id)
        {
            if(SuperheroHandler.GetSuperheroes().Any(x=>x.UniverseId == id))
            {
                throw new Exception("Unable to delete universe. 1 or more superheroes uses this universe");
            }
            Datastore.UniverseStore.DeleteOne(id);
        }
    }
}
