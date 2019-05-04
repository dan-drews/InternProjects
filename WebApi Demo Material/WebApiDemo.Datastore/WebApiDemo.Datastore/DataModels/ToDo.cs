using System;

namespace WebApiDemo.Datastore.DataModels
{
    public class ToDo
    {
        public int Id { get; set; }

        public Enums.ToDoEnums.Status Status { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateUpdated { get; set; }

        public string AdditionalNotes { get; set; }
        
    }
}
