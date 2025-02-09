﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Template.Domain.Interfaces;

namespace Template.Domain
{
	public class City : IEntityWithName
    {
		public Guid Id { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int StateId { get; set; }

        public State? State { get; set; }

    }
}

