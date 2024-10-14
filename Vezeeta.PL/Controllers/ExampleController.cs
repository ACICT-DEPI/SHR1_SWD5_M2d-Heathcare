using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;

namespace Vezeeta.PL.Controllers
{
    //to use special method
    //var example2 = await _unitOfWork.ExampleRepository.SpecialMethod();

    public class ExampleController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ExampleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Example
        public async Task<IActionResult> Index()
        {
            var examples = await _unitOfWork.Repository<Example>().GetAllAsync();
            return View(examples);  
        }

        // GET: Example /Details/1
        public async Task<IActionResult> Details(int id)
        {
            var example = await _unitOfWork.Repository<Example>().GetByIdAsync(id);
            if (example == null)
            {
                return NotFound();
            }
            return View(example);  
        }

        // GET: Example/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }

        // POST: Example/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Example example)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Repository<Example>().AddAsync(example);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));  
            }
            return View(example); 
        }

        // GET: Example/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var example = await _unitOfWork.Repository<Example>().GetByIdAsync(id);
            if (example == null)
            {
                return NotFound();
            }
            return View(example); 
        }

        // POST: Example/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Example example)
        {
            if (id != example.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                 _unitOfWork.Repository<Example>().UpdateAsync(example);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));  
            }
            return View(example);  
        }

        // GET: Example/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var example = await _unitOfWork.Repository<Example>().GetByIdAsync(id);
            if (example == null)
            {
                return NotFound();
            }
            return View(example);  
        }

        // POST: Example/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var example = _unitOfWork.Repository<Example>().GetByIdAsync(id);
            if (example == null)
            {
                return NotFound();
            }
            await _unitOfWork.Repository<Example>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();
             return RedirectToAction(nameof(Index)); 
        }
    }
}
