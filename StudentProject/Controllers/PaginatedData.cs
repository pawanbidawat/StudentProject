using StudentProject.Models;

namespace StudentProject.Controllers
{
    internal class PaginatedData<T>
    {
        public List<StudentData> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
    }
}