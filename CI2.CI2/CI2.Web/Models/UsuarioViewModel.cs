using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CI2.Web.Models
{
    public class UsuarioViewModel
    {
        //Usuario
        public string IdUsuario { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public bool CorreoConfirmacion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public bool TelefonoConfirmacion { get; set; }
        [Required]
        public bool Bloqueo { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        //RolUsuario
        
        [Display(Name ="Rol")]
        [Required]
        public string IdRol { get; set; }
        public string NombreRol { get; set; }


    }
}