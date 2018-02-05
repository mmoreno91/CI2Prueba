using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CI2.Web.Models
{
    /// <summary>
    /// Determina el tipo de resultado de la operación consultar una tarea
    /// </summary>
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

    /// <summary>
    /// Enumerados de estados de una tarea
    /// </summary>
    public enum Estados
    {
        /// <summary>
        /// Cunado una tarea se encuentra finalizada 1
        /// </summary>
        finalizada,
        /// <summary>
        /// Cuando una tarea se encuentra en proceso 0
        /// </summary>
        pendiente,
        /// <summary>
        /// Cuando una tarea se encuentra finalizada 0
        /// </summary>
        sinespecificar
    }

    /// <summary>
    /// Enumerado de maneras de ordenar una tarea
    /// </summary>
    public enum Ordenar
    {
        /// <summary>
        /// asc
        /// </summary>
        ascendente,
        /// <summary>
        /// desc
        /// </summary>
        descendente,
        /// <summary>
        /// asc
        /// </summary>
        sinespecificar
    }

    public class ConsultaParametrosViewModel
    {
        /// <summary>
        /// Id del usuario autenticado
        /// </summary>
        public string Usuario { get; set; } = null;
        /// <summary>
        /// Enumerado de estados, de tipo pendiente o finalizada la tarea según corresponda
        /// </summary>
        public Estados Estado { get; set; } = Estados.finalizada;

        /// <summary>
        /// Determina la forma de ordenar los registros, puede ser de tipo descendiente o ascendiente
        /// </summary>
        public Ordenar Ordenar { get; set; } = Ordenar.ascendente;
    }


    public class CrearParametrosViewModel
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string FechaVencimiento { get; set; }
        [Required]
        public Estados Estado { get; set; }
    }

    public class CrearResultadoViewModel : ConsultaResultadoViewModel
    {

    }

    public class ActualizarParametrosViewModel
    {
        [Required]
        public Int64 IdTarea { get; set; }
        
        public string Descripcion { get; set; }
        
        public string FechaVencimiento { get; set; }
        
        public Estados Estado { get; set; }
    }

    public class ActualizarResultadoViewModel : ConsultaResultadoViewModel
    {

    }

    public class BorrarParametrosViewModel
    {
        [Required]
        public Int64 IdTarea { get; set; }
    }

    public class BorrarResultadoViewModel 
    {

    }
}

