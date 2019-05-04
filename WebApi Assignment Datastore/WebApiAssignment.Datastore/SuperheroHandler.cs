using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiAssignment.Datastore.DataModels;

namespace WebApiAssignment.Datastore
{
    public static class SuperheroHandler
    {
        public static List<Superhero> GetSuperheroes() => Datastore.SuperheroStore.AsQueryable().ToList();

        public static Superhero GetSuperhero(int id) => Datastore.SuperheroStore.Find(x => x.Id == id).FirstOrDefault() ?? throw new Exception($"No Superhero found with ID: {id}");

        public static void DeleteSuperhero(int id) => Datastore.SuperheroStore.DeleteOne(id);

        public static void AddSuperhero(Superhero superhero)
        {
            ValidateExistenceOfUniverse(superhero.UniverseId);
            Datastore.SuperheroStore.InsertOne(superhero);
        }

        public static void UpdateSuperhero(Superhero superhero)
        {
            ValidateExistenceOfUniverse(superhero.UniverseId);
            Datastore.SuperheroStore.UpdateOne(x => x.Id == superhero.Id, superhero);
        }

        private static void ValidateExistenceOfUniverse(int universeId)
        {
            UniverseHandler.GetUniverse(universeId);
        }
    }
}
