using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers;

public class TodosController : Controller
{
    private static List<TodoItem> _todos = new()
    {
        new TodoItem { Id = 1, Title = "Learn ASP.NET Core", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Build a Todo MVC App", IsCompleted = true }
    };

    private static int _nextId = 3;

    // GET: /Todos or /Todos/Index
    public IActionResult Index()
    {
        return View(_todos);
    }

    // GET: /Todos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Todos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TodoItem todoItem)
    {
        if (ModelState.IsValid)
        {
            todoItem.Id = _nextId++;
            todoItem.CreatedAt = DateTime.UtcNow;
            _todos.Add(todoItem);
            return RedirectToAction(nameof(Index));
        }
        return View(todoItem);
    }

    // GET: /Todos/Edit/5
    public IActionResult Edit(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
            return NotFound();

        return View(todo);
    }

    // POST: /Todos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
            return BadRequest();

        if (ModelState.IsValid)
        {
            var existing = _todos.FirstOrDefault(t => t.Id == id);
            if (existing == null)
                return NotFound();

            existing.Title = todoItem.Title;
            existing.IsCompleted = todoItem.IsCompleted;

            return RedirectToAction(nameof(Index));
        }
        return View(todoItem);
    }

    // GET: /Todos/Delete/5
    public IActionResult Delete(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
            return NotFound();

        return View(todo);
    }

    // POST: /Todos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
            _todos.Remove(todo);

        return RedirectToAction(nameof(Index));
    }
}