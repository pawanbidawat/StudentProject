using StudentProject.Models;

namespace StudentProject.Repo
{
    public interface IVariables
    {
        public IQueryable<StudentData> SearchResult(string search, IQueryable<StudentData> query);
         public string Search { get; set; }
         public int Page { get; set; }
        public int PageCount { get; set; }

         public string hello { get; set; }
    }
}
