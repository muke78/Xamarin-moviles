using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SQLite.Modelos
{
    public class ProductosM
    {
        [PrimaryKey, AutoIncrement]
        public int idproducto { get; set; }

        [MaxLength(30)]
        public String nombreproducto { get; set; }

        [MaxLength(30)]
        public String compañia { get; set; }

        [MaxLength(30)]
        public String usuario { get; set; }

        [MaxLength(80)]
        public String Fecha { get; set; }

        public ProductosM()
        {

        }
    }
}
