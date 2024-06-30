using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Navegacion.Modelos
{
    public class Generosm
    {
        [PrimaryKey, AutoIncrement]
        public int matricula { get; set; }

        [MaxLength(120)]
        public String genero { get; set; }

       

        public Generosm()
        {

        }
    }
}
