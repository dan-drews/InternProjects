using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.ResponseObjects
{
    public class ToDoListResponseModel
    {
        public int Count { get; set; }
        public List<ToDoListItemResponseModel> Items { get; set; } = new List<ToDoListItemResponseModel>();
    }

    public  class ToDoListItemResponseModel
    {
        public int ToDoId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
