using Microsoft.AspNetCore.Mvc;

using StudentProject.Data;
using StudentProject.Models;



namespace StudentProject.Controllers
{
    public class StudentDataController : Controller
    {
        private readonly StudentDbContext _db;
        public StudentDataController(StudentDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(string search,  int? page )
        {
            var query = _db.Students.AsQueryable();

            int pageSize = 2;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.FirstName.Contains(search)
                                          || s.LastName.Contains(search)
                                          || s.Course.Contains(search)
                                          || s.Email.Contains(search)
                                          || s.Phone.Contains(search)
                                          || s.Gender.StartsWith(search));


            }        

            //paging
            int pageCount = (int)Math.Ceiling(query.Count() / (double)pageSize);
            int currentPage = page ?? 1;
            var paged = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
         
            ViewData["Search"] = search;
            ViewData["Page"] = currentPage;
            ViewData["PageCount"] = pageCount;


            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Index", paged);
            }
            else
            {
                return View(paged);
            }


        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentData obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "New data has been created";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if(id == 0 || id==null) {
               return NotFound();
            }

            StudentData? student = _db.Students.Find(id);
            if(student == null)
            {
               return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(StudentData obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Data is updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            StudentData? student = _db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            StudentData? obj = _db.Students.Find(id);

            if(obj == null)
            {
                return NotFound();
            }

            _db.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Data deleted successfully";
            return RedirectToAction("Index");

            
        }

        public IActionResult Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            StudentData? student = _db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        //Adding Searching Index Action

      


    }
}
