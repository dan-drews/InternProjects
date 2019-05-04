using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using WebApiAssignment.Datastore.DataModels;

[assembly: InternalsVisibleTo("WebApiAssignment.Datastore.Test")]
namespace WebApiAssignment.Datastore
{
    internal static class Datastore
    {
        internal static JsonFlatFileDataStore.DataStore Store = new JsonFlatFileDataStore.DataStore("data.json");

        internal static IDocumentCollection<Universe> UniverseStore => Store.GetCollection<Universe>();

        internal static IDocumentCollection<Superhero> SuperheroStore => Store.GetCollection<Superhero>();
    }
}
