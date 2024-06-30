using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SQLite.Modelos
{
    public class Alumnos
    {
        [PrimaryKey, AutoIncrement]
        public int matricula { get; set; }

        [MaxLength(30)]
        public String nombre { get; set; }

        [MaxLength(30)]
        public String apellidoPaterno { get; set; }

        [MaxLength(30)]
        public String apellidoMaterno { get; set; }

        [MaxLength(80)]
        public String correo { get; set; }


        public Alumnos()
        {

        }
    }
}
