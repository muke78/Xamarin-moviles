using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navegacion.Modelos
{
    public class Empleados
    {
        [PrimaryKey, AutoIncrement]
        public int matricula { get; set; }

        [MaxLength(30)]
        public String nombre { get; set; }

        [MaxLength(30)]
        public String apellidopaterno { get; set; }

        [MaxLength(30)]
        public String apellidomaterno { get; set; }
        [MaxLength(20)]
        public String numero { get; set; }

        [MaxLength(120)]
        public String direccion { get; set; }


        public Empleados()
        {
        }
    }
}
