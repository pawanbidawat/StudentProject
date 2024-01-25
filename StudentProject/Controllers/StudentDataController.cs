using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Data;
using StudentProject.Models;
using StudentProject.Repo;
using System.Diagnostics;


namespace StudentProject.Controllers
{
    public class StudentDataController : Controller
    {
        private readonly StudentDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //[FromServices]
        //public IWorld _repo { get; set; }



        //[FromServices]
        public IVariables _var;

        public StudentDataController(StudentDbContext db, IWebHostEnvironment webHostEnvironment, IVariables var)
        {
            _db = db;
            _var = var;
            _webHostEnvironment = webHostEnvironment;

        }


        public IActionResult Index(string search, int? page)
        {

            var query = _var.SearchResult(search, _db.Students.AsQueryable());
            int pageSize = 3;



            //paging
            int pageCount = (int)Math.Ceiling(query.Count() / (double)pageSize);
            int currentPage = page ?? 1;
            var paged = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            _var.Search = search;
            _var.Page = currentPage;
            _var.PageCount = pageCount;


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
                StoreImage(obj);


                _db.Students.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "New data has been created";
                return RedirectToAction("Index");
            }
            return View();
        }

        public void StoreImage(StudentData obj)
        {
            if (obj.Image != null && obj.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var FileName = obj.FirstName.ToString() + "_" + obj.Email.ToString() + "_" + obj.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, FileName);

                if (Directory.GetFiles(uploadsFolder, Path.GetFileNameWithoutExtension(FileName) + "*").Any())
                {
                    var uni_Image = $"{Path.GetFileNameWithoutExtension(FileName)}_{DateTime.Now.ToString("yyyyMMddHHmm")}_{Path.GetExtension(FileName)}";
                    filePath = Path.Combine(uploadsFolder, uni_Image);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.Image.CopyTo(fileStream);
                }


            }

        }




        public IActionResult Edit(int? id)
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

        [HttpPost]
        public IActionResult Edit(StudentData obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                StoreImage(obj);
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
        public IActionResult DeletePost(int? id, string password)
        {
            StudentData? obj = _db.Students.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            if (password != _var.Password)
            {
                // Password is incorrect, handle accordingly
                ModelState.AddModelError("Password", "Incorrect password");
                return View(obj); // Return to the delete confirmation view with the error message
            }

            // Password is correct, proceed with deletion
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


        public IActionResult Gallery(StudentData obj)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            var filePath = Directory.GetFiles(uploadsFolder);

            List<string> imagePath = new List<string>();

            foreach (var image in filePath)
            {

                imagePath.Add(Path.GetFileName(image));


            }
            return View(imagePath);
        }




    }
}
