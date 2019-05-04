using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.RequestObjects;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        [HttpGet]
        public ResponseObjects.ToDoListResponseModel ToDos()
        {
            var list = Datastore.ToDoHandler.GetToDos();
            var resp = new ResponseObjects.ToDoListResponseModel();
            resp.Count = list.Count;
            resp.Items = list.Select(x => new ResponseObjects.ToDoListItemResponseModel()
            {
                IsCompleted = x.Status == Datastore.Enums.ToDoEnums.Status.Complete,
                Title = x.Title,
                ToDoId = x.Id
            }).ToList();
            return resp;
        }

        [HttpPost]
        public Datastore.DataModels.ToDo AddToDo([FromBody]AddToDoRequestModel requet)
        {
            var td = new Datastore.DataModels.ToDo() { Title = requet.Title };
            td.Status = Datastore.Enums.ToDoEnums.Status.Incomplete;
            Datastore.ToDoHandler.InsertToDo(td);
            return td;
        }
    }
}