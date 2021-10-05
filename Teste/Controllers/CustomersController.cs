using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Data;
using Teste.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teste.Controllers
{
    [Route("v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<ActionResult<Customer>> Create(string name, string cpf)
        {
            Customer customer = new(name, cpf);
            if (ModelState.IsValid && _context.Customers.Where(c => c.Cpf == customer.Cpf).Any() == false)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetById", new { id = customer.Id }, customer);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
