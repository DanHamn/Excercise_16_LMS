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
    class ModuleRepository : IModuleRepository
    {
        private readonly LmsApiContext db;
        public ModuleRepository(LmsApiContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(Module @module)
        {
            db.Add(@module);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Module.AnyAsync(g => g.Id == id);
        }

        public async Task<Module> FindAsync(int? id)
        {
            return await db.Module.FindAsync(id);
        }

        public async Task<IEnumerable<Module>> GetAllModuless()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module> GetModules(int? id)
        {
            return await db.Module
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Remove(Module @module)
        {
            db.Module.Remove(@module);
        }

        public void Update(Module @module)
        {
            db.Module.Update(@module);
        }
    }
}
