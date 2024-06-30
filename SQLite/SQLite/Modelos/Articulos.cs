using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SQLite.Modelos
{
    public class Articulos
    {
        [PrimaryKey, AutoIncrement]
        public int idarticulo { get; set; }

        [MaxLength(30)]
        public String titulo { get; set; }

        [MaxLength(30)]
        public String usuario { get; set; }

        [MaxLength(30)]
        public String fecha { get; set; }

        [MaxLength(500)]
        public String informacion { get; set; }

        public Articulos()
        {

        }
    }
}
