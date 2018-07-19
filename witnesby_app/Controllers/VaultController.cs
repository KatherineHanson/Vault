using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WitnesbyApp.Models;

namespace WitnesbyApp.Controllers
{
    [Route("api/vault")]
    [ApiController]
    public class VaultController : ControllerBase
    {
        private readonly VaultContext _context;

        public VaultController(VaultContext context)
        {
            _context = context;

            if (_context.VaultItems.Count() == 0)
            {
                _context.VaultItems.Add(new VaultItem { Description = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<VaultItem>> GetAll()
        {
            return _context.VaultItems.ToList();
        }

        [HttpGet("{id}", Name = "GetVault")]
        public ActionResult<VaultItem> GetById(long id)
        {
            var item = _context.VaultItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(VaultItem item)
        {
            _context.VaultItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetVault", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, VaultItem item)
        {
            var vault = _context.VaultItems.Find(id);
            if (vault == null)
            {
                return NotFound();
            }

            vault.Description = item.Description;

            _context.VaultItems.Update(vault);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var vault = _context.VaultItems.Find(id);
            if (vault == null)
            {
                return NotFound();
            }

            _context.VaultItems.Remove(vault);
            _context.SaveChanges();
            return NoContent();
        }
    }
}