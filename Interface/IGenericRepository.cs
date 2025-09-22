using System.Collections.Generic;
using System.Threading.Tasks;
using MvcApp.Models;

namespace MvcApp.Interface
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();

        Task<Course> GetByIdAsync(int id);

        Task AddAsync(Course course);

        void Update(Course course);

        void Delete(Course course);

        Task<int> SaveAsync();
    }
}
