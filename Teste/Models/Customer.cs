using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Teste.Models
{
    [Index(nameof(Cpf))]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(30, ErrorMessage = "O nome deve conter entre 2 e 30 caracteres.")]
        [MinLength(2, ErrorMessage = "O nome deve conter entre 2 e 30 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(11, ErrorMessage = "O CPF deve conter 11 dígitos.")]
        [MinLength(11, ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string Cpf { get; set; }

        public Customer()
        {
        }

        public Customer(string name, string cpf)
        {
            Name = name;
            Cpf = cpf;
        }
    }
}
