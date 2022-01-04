using Microsoft.EntityFrameworkCore;

namespace ErkerCore.DataAccessLayer
{
    public class BaseRepository
    {

        static MainDbContext curr = null;

        public static T GetDbContext<T>() where T : DbContext, new()
        {
            return new T();
        }
        public static DbContext GetCurrentDbContext()
        {
            if (curr == null)
            {
                curr = BaseRepository.GetDbContext<MainDbContext>();
            }
            return curr;
        }
    }
}