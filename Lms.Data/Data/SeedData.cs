using Bogus;
using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Data
{
    public class SeedData
    {
        private static readonly Faker faker = new("sv");
        private static readonly TextInfo ti = new CultureInfo("sv-SE", false).TextInfo;
        public static async Task InitAsync(LmsApiContext context)
        {
            if (context is null) throw new NullReferenceException(nameof(LmsApiContext));

            //if (await context.Course.AnyAsync()) return;

            var courses = GetCoursesAsync(5);
            await context.AddRangeAsync(courses);

            foreach (var course in courses)
            {
                var modules = GetModulesAsync(course, faker.Random.Int(1, 5));
                await context.AddRangeAsync(modules);
            }

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Course> GetCoursesAsync(int v)
        {
            var courses = new List<Course>();

            for (int i = 0; i < v; i++)
            {
                var temp = new Course
                {
                    Title = ti.ToTitleCase(faker.Hacker.Noun() + " " + faker.Hacker.Verb()),
                    StartDate = DateTime.Now.AddDays(faker.Random.Int(-5, 5))
                };
                courses.Add(temp);
            }

            return courses;
        }

        private static IEnumerable<Module> GetModulesAsync(Course course, int v)
        {
            var modules = new List<Module>();

            for (int i = 0; i < v; i++)
            {
                var temp = new Module
                {
                    Title = ti.ToTitleCase(faker.Hacker.Noun() + " " + faker.Hacker.Verb()),
                    StartDate = DateTime.Now.AddDays(faker.Random.Int(-5, 5)),
                    Course = course
                };
                modules.Add(temp);
            }

            return modules;
        }
    }
}
