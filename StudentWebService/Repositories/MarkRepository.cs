using StudentWebService.Models;

namespace StudentWebService.Repositories
{
    public class MarkRepository : BaseRepository<Mark>
    {
        public MarkRepository() : base("Mark")
        {
        }
    }
}