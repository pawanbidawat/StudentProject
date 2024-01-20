using StudentProject.Models;

namespace StudentProject.Repo
{
    public class Queryable<T>
    {
        internal Queryable<StudentData> Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}