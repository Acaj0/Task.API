using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.API.Models;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static readonly List<TodoItem> _todoItems = new List<TodoItem>();
        [HttpGet]

        public ActionResult <IEnumerable<TodoItem>> Get()
        {
            return Ok(_todoItems);
        }
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
                if(todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }
        [HttpPost]
        public ActionResult Post([FromBody] TodoItem todoItem)
        {
            _todoItems.Add(todoItem);
            return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            var todoItemToUpdate = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemToUpdate == null)
            {
                return NotFound();
            }
            todoItemToUpdate.Title = todoItem.Title;
            todoItemToUpdate.Description = todoItem.Description;
            todoItemToUpdate.IsCompleted = todoItem.IsCompleted;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItemToDelete = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItemToDelete==null)
            {
                return NotFound();
            }
            _todoItems.Remove(todoItemToDelete);

            return NoContent();
        }
    }
}
