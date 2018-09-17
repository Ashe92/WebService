using StudentWebService.Models;

namespace StudentWebService.Repositories
{
    public class CourseRepository :BaseRepository<Course>
    {
        public CourseRepository() : base("Course")
        {
        }
    }
}