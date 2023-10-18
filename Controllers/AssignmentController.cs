using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace DSW_ToDo_MVC.Controllers
{
  public class AssignmentController : Controller
  {
    private readonly AssignmentService _assignmentService;

    public AssignmentController(AssignmentService assignmentService)
    {
      _assignmentService = assignmentService;
    }

    // GET: Assignment
    public async Task<IActionResult> Index()
    {
      return View(await _assignmentService.GetAsync());
    }

    // GET: Assignment/Details/5
    public async Task<IActionResult> Details(string id)
    {
      var assignment = await _assignmentService.GetAsync(id);

      if (assignment == null)
      {
        return NotFound();
      }

      return View(assignment);
    }

    // GET: Assignment/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Assignment/Create
    [HttpPost]
    public async Task<IActionResult> Create(Assignment newAssignment)
    {
      await _assignmentService.CreateAsync(newAssignment);
      CreatedAtAction(nameof(Details), new { id = newAssignment.Id }, newAssignment);
      return RedirectToAction(nameof(Index));
    }

    // GET: Assignment/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var assignment = await _assignmentService.GetAsync(id);
      if (assignment == null)
      {
        return NotFound();
      }
      return View(assignment);
    }

    // POST: Assignment/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Assignment updatedAssignment)
    {
      var assignment = await _assignmentService.GetAsync(id);
      if (assignment is null)
      {
        return NotFound();
      }

      updatedAssignment.Id = assignment.Id;

      await _assignmentService.UpdateAsync(id, updatedAssignment);

      return RedirectToAction(nameof(Index));
    }

    // GET: Assignment/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var assignment = await _assignmentService.GetAsync(id);
      if (assignment == null)
      {
        return NotFound();
      }

      return View(assignment);
    }

    // POST: Assignment/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var assignment = await _assignmentService.GetAsync(id);

      if (assignment is null)
      {
        return NotFound();
      }

      await _assignmentService.RemoveAsync(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
