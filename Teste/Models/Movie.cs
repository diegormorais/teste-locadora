using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Teste.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(30, ErrorMessage = "O título do filme deve conter entre 2 e 30 caracteres.")]
        [MinLength(2, ErrorMessage = "O título do filme deve conter entre 2 e 30 caracteres.")]
        public string Title { get; set; }
        public bool Avaiable { get; set; }

        public Movie()
        {
        }
        public Movie(string title)
        {
            Title = title;
            Avaiable = true;
        }
    }
}
