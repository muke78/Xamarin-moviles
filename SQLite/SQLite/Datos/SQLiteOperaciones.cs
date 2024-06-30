using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Modelos;

namespace SQLite.Datos
{
    public class SQLiteOperaciones
    {
        SQLiteAsyncConnection db;
        SQLiteAsyncConnection db2;
        SQLiteAsyncConnection db3;

        public SQLiteOperaciones(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Alumnos>().Wait();

            db2 = new SQLiteAsyncConnection(dbPath);
            db2.CreateTableAsync<Articulos>().Wait();

            db3 = new SQLiteAsyncConnection(dbPath);
            db3.CreateTableAsync<ProductosM>().Wait();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //tabla alumnos
        public Task<int> GuardarAlumno(Alumnos alum)
        {
            if(alum.matricula!=0)
            {
                return db.UpdateAsync(alum);
            }
            else
            {
                return db.InsertAsync(alum);
            }
        }

        public Task<List<Alumnos>> GetAlumnos()
        {
            return db.Table<Alumnos>().ToListAsync();
        }
        public Task<int> BorrarAlumno(Alumnos alum)
        {
            return db.DeleteAsync(alum);
  
        }
        public Task<Alumnos> GetAlumnoByIdAsync(int matricula)
        {
            return db.Table<Alumnos>().Where(a => a.matricula == matricula).FirstOrDefaultAsync();

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //tabla articulos
        public Task<int> GuardarArticulo(Articulos art)
        {
            if (art.idarticulo != 0)
            {
                return db.UpdateAsync(art);
            }
            else
            {
                return db2.InsertAsync(art);
            }
        }

        public Task<List<Articulos>> GetArticulo()
        {
            return db2.Table<Articulos>().ToListAsync();
        }

        public Task<int> BorrarArticulo(Articulos art)
        {
            return db2.DeleteAsync(art);
        }

        public Task<Articulos> GetArticuloByIdAsync(int idarticulo)
        {
            return db2.Table<Articulos>().Where(a => a.idarticulo == idarticulo).FirstOrDefaultAsync();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //tabla productos
        public Task<int> GuardarProductos(ProductosM prod)
        {
            if (prod.idproducto != 0)
            {
                return db3.UpdateAsync(prod);
            }
            else
            {
                return db3.InsertAsync(prod);
            }
        }

        public Task<List<ProductosM>> GetProductos()
        {
            return db3.Table<ProductosM>().ToListAsync();
        }

        public Task<int> BorrarProductos(ProductosM art)
        {
            return db3.DeleteAsync(art);
        }

        public Task<ProductosM> GetProductosByIdAsync(int idproducto)
        {
            return db3.Table<ProductosM>().Where(a => a.idproducto == idproducto).FirstOrDefaultAsync();
        }
    }
}
