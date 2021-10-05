using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Data;
using Teste.Models;

namespace Teste.Controllers
{

    [Route("v1/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> Get()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Create(string title)
        {
            Movie movie = new(title);
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new { id = movie.Id }, movie);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
