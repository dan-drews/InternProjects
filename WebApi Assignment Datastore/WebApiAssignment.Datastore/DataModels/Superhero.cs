using System.Collections.Generic;

namespace WebApiAssignment.Datastore.DataModels
{
    public class Superhero
    {
        public int Id { get; set; }
        public string SuperheroName { get; set; }
        public string SecretIdentity { get; set; }
        public int? AgeAtOrigin { get; set; }
        public int YearOfAppearance { get; set; }
        public bool Alien { get; set; }
        public string OriginStory { get; set; }
        public int UniverseId { get; set; } = -1;
        public List<string> Abilities { get; set; }
    }
}
