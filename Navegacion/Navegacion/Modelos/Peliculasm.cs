using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Navegacion.Modelos
{
    public class Peliculasm
    {

        [PrimaryKey, AutoIncrement]
        public int matricula { get; set; }

        [MaxLength(80)]
        public String nombrePelicula { get; set; }

        [MaxLength(30)]
        public String usuario { get; set; }

        [MaxLength(80)]
        public String genero { get; set; }

        [MaxLength(5)]
        public String año { get; set; }

        public Peliculasm()
        {

        }
    }
}
