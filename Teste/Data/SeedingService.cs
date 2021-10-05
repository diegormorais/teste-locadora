using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;

namespace Teste.Data
{
    public class SeedingService
    {
        private DataContext _context;
        public SeedingService(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Customers.Any() ||
                _context.Rentals.Any() ||
                _context.Movies.Any())
            {
                return; // DB has been seeded
            }

            Customer c1 = new("Diego", "11111111111");
            Customer c2 = new("Teste", "22222222222");

            Movie m1 = new("Duna");
            Movie m2 = new("Matrix");

            Rental r1 = new(1, 1, new DateTime(2021, 5, 2));
            Rental r2 = new(1, 1, new DateTime(2021, 10, 6));
            Rental r3 = new(1, 1, new DateTime(2021, 10, 20));

            _context.Customers.AddRange(c1, c2);
            _context.Movies.AddRange(m1, m2);
            _context.Rentals.AddRange(r1, r2, r3);
            _context.SaveChanges();
        }
    }
}
