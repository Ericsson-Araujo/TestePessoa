using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestePessoaWeb.Domain
{
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(70), Required]
        public string Nome { get; set; }
        [MaxLength(14), Required]
        public string CPF { get; set; }
        [MaxLength(100), Required]
        public string Email { get; set; }
        [MaxLength(11)]
        public string Telefone { get; set; }
        [MaxLength(1)]
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
