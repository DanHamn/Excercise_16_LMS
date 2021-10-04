using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext db;
        public CourseRepository(LmsApiContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Course course)
        {
            db.Add(course);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Course.AnyAsync(g => g.Id == id);
        }

        public async Task<Course> FindAsync(int? id)
        {
            return await db.Course.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await db.Course.ToListAsync();
        }

        public async Task<Course> GetCourse(int? id)
        {
            return await db.Course
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Remove(Course course)
        {
            db.Course.Remove(course);
        }

        public void Update(Course course)
        {
            db.Course.Update(course);
        }
    }
}
