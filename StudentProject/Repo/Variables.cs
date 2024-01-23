using Microsoft.AspNetCore.Identity;
using StudentProject.Models;


namespace StudentProject.Repo
{
    public class Variables : IVariables
    {
        public IQueryable<StudentData> SearchResult(string search, IQueryable<StudentData> query)
        {
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.FirstName.Contains(search)
                                          || s.LastName.Contains(search)
                                          || s.Course.Contains(search)
                                          || s.Email.Contains(search)
                                          || s.Phone.Contains(search)
                                          || s.Gender.StartsWith(search));

             

            }
            return query;
        }
         public string Search { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public string Password { get; set; } = "poco";

        public string hello { get; set; }
    }
}
