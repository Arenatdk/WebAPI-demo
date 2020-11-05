using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly WebApiContext _context;

        public TicketsController(WebApiContext context)
        {
            _context = context;
            if (!_context.Tickets.Any())
            {
                User usr = new User { Name = "Alice", Age = 31 };
                _context.Users.Add(usr);
                
                _context.Tickets.Add(new Ticket { Name = "Tom", User = usr});
                _context.Tickets.Add(new Ticket { Name = "Alice", User = usr });
                _context.SaveChanges();
            }
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> Get()
        {
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> Get(int id)
        {
            Ticket ticket = await _context.Tickets.Include(p=>p.User).FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> Put(Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }
            if (!_context.Tickets.Any(x => x.Id == ticket.Id))
            {
                return NotFound();
            }

            _context.Update(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        // POST: api/Tickets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ticket>> Post(Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticket>> Delete(int id)
        {
            Ticket ticket = _context.Tickets.FirstOrDefault(x => x.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

    }
}
