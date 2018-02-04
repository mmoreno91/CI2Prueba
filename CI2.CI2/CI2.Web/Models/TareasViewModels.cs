using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CI2.Web.Models
{
    public class ConsultaResultadoViewModel
    {
        public Int64 IdTarea { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string IdUsuario { get; set; }
    }

    public enum Estados
    {
        finalizada,
        pendiente,
        sinespecificar
    }

    public enum Ordenar
    {
        ascendente,
        descendente,
        sinespecificar
    }

    public class ConsultaParametrosViewModel
    {
        public string Usuario { get; set; } = null;

        public Estados Estado { get; set; } = Estados.finalizada;

        public Ordenar Ordenar { get; set; } = Ordenar.ascendente;
    }


    public class CrearParametrosViewModel
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string FechaVencimiento { get; set; }
        [Required]
        public string Estado { get; set; }
    }

    public class CrearResultadoViewModel : ConsultaResultadoViewModel
    {

    }
}

