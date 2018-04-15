using System;
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models
{
    public class Promo
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Promo()
        {
            Id = Guid.NewGuid();
        }
    }
}