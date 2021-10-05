using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste.Data;
using Teste.Models;

namespace Teste.Controllers
{
    [Route("v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly DataContext _context;

        public RentalsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals.ToListAsync();
        }

        // GET: api/Rentals/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return rental;
        }

        // POST: api/Rentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rental>> CreateRental(Rental obj)
        {
            if (MovieExists(obj.MovieId) && CustomerExists(obj.CustomerId) && ModelState.IsValid && _context.Movies.Find(obj.MovieId).Avaiable == true)
            {
                _context.Add(obj);
                _context.Movies.Find(obj.MovieId).Avaiable = false;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetRental", new { id = obj.Id }, obj);
            }
            return BadRequest();
        }

        [HttpPut("closeRental/{id}")]
        public async Task<ActionResult<Rental>> CloseRental(int id)
        {
            if (!RentalExists(id))
            {
                return NotFound();
            }
            Rental rental = _context.Rentals.Find(id);
            rental.IsReturned = true;
            if (rental.ReturnDate < DateTime.Today)
            {
                rental.Penalty = true;
            }
            _context.Movies.Find(rental.MovieId).Avaiable = true;

            try
            {
                await _context.SaveChangesAsync();
                return AcceptedAtAction("GetRental", new { id = rental.Id }, rental);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
