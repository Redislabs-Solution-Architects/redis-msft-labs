using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using Redis.OM.Searching;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly RedisCollection<RedisTodoItem> _todos;

        public TodoItemsController(RedisConnectionProvider provider)
        {
            _todos = (RedisCollection<RedisTodoItem>)provider.RedisCollection<RedisTodoItem>();
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RedisTodoItem>>> GetTodoItems()
        {
            var todos = await _todos
            .OrderBy(todo => todo.IsComplete)
            .ToListAsync();
            
            return Ok(todos);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RedisTodoItem>> GetTodoItem(string id)
        {
            var todoItem = await _todos.FindByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(string id, RedisTodoItem todoItem)
        {
            var todo = _todos.FindById(id.ToString());

            if (todo == null)
            {
                return NotFound();
            }
            
            todo.Name = todoItem.Name;
            todo.IsComplete = todoItem.IsComplete;

            await _todos.SaveAsync();

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RedisTodoItem>> PostTodoItem(RedisTodoItem todoItem)
        {
            await _todos.InsertAsync(todoItem);
            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(string id)
        {
            var todoItem = await _todos.FindByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            await _todos.DeleteAsync(todoItem);

            return NoContent();
        }
    }
}
