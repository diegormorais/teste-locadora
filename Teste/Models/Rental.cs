using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace Teste.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

       // public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Nenhum filme escolhido.")]    
        public int MovieId { get; set; }

        //public Movie Movie { get; set; }

        [Required(ErrorMessage = "Data de devolução não informada.")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ReturnDate { get; set; }

        public bool IsReturned { get; set; }
        public bool Penalty { get; set; }

        public Rental()
        {
        }
        public Rental(int customerId, int movieId, DateTime returnDate)
        {
            CustomerId = customerId;
            MovieId = movieId;
            ReturnDate = returnDate;
            IsReturned = false;
            Penalty = false;
        }

    }
}
