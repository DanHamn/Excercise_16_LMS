using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Repositories;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUoW uow;

        public ModulesController(LmsApiContext context, IUoW unitOfWork)
        {
            _context = context;
            uow = unitOfWork;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            //return await _context.Module.ToListAsync();
            return (ActionResult)await uow.ModuleRepository.GetAllModuless();
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            //var @module = await _context.Module.FindAsync(id);
            var @module = await uow.ModuleRepository.FindAsync(id) ;

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            _context.Entry(@module).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ModuleExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
            //_context.Module.Add(@module);
            //await _context.SaveChangesAsync();
            uow.ModuleRepository.Add(@module);
            await uow.CompleteAsync();

            return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            //var @module = await _context.Module.FindAsync(id);
            var @module = await uow.ModuleRepository.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            //_context.Module.Remove(@module);
            //await _context.SaveChangesAsync();
            uow.ModuleRepository.Remove(@module);
            await uow.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> ModuleExistsAsync(int id)
        {
            //return _context.Module.Any(e => e.Id == id);
            return await uow.ModuleRepository.AnyAsync(id);
        }
    }
}
