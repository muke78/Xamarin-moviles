using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Navegacion.Modelos;
namespace Navegacion.BASEDATOS
{
    public class SQLiteOperaciones
    {
        SQLiteAsyncConnection db;

        SQLiteAsyncConnection db2;

        SQLiteAsyncConnection db3;

        SQLiteAsyncConnection db4;

        public SQLiteOperaciones(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Empleados>().Wait();

            db2 = new SQLiteAsyncConnection(dbPath);
            db2.CreateTableAsync<Seriesm>().Wait();

            db3 = new SQLiteAsyncConnection(dbPath);
            db3.CreateTableAsync<Peliculasm>().Wait();

            db4 = new SQLiteAsyncConnection(dbPath);
            db4.CreateTableAsync<Generosm>().Wait();

        }
        ////////////////////////////////
        ///Tabla usuarios
        public Task<int> GuardarAlumno(Empleados emp)
        {
            if (emp.matricula != 0)
            {
                return db.UpdateAsync(emp);
            }
            else
            {
                return db.InsertAsync(emp);
            }
        }

        public Task<List<Empleados>> GetAlumnos()
        {
            return db.Table<Empleados>().ToListAsync();
        }

        public Task<int> BorrarEmpleado(Empleados emp)
        {
            return db.DeleteAsync(emp);

        }
        public Task<Empleados> GetEmpleadoByIdAsync(int matricula)
        {
            return db.Table<Empleados>().Where(a => a.matricula == matricula).FirstOrDefaultAsync();

        }

        ////////////////////////////////////////////////////////////////
        ///Tabla series
        public Task<int> GuardarSerie(Seriesm ser)
        {
            if (ser.matricula != 0)
            {
                return db2.UpdateAsync(ser);
            }
            else
            {
                return db2.InsertAsync(ser);
            }
        }

        public Task<List<Seriesm>> Getseries()
        {
            return db2.Table<Seriesm>().ToListAsync();
        }

        public Task<int> BorrarSerie(Seriesm ser)
        {
            return db2.DeleteAsync(ser);
        }

        public Task<Seriesm> GetSerieByIdAsync(int matricula)
        {
            return db2.Table<Seriesm>().Where(a => a.matricula == matricula).FirstOrDefaultAsync();
        }
        //////////////////////////////////////////////////
        ///Tabla peliculas
        public Task<int> GuardarPelicula(Peliculasm pel)
        {
            if (pel.matricula != 0)
            {
                return db3.UpdateAsync(pel);
            }
            else
            {
                return db3.InsertAsync(pel);
            }
        }

        public Task<List<Peliculasm>> GetPelicula()
        {
            return db3.Table<Peliculasm>().ToListAsync();
        }

        public Task<int> BorrarPelicula(Peliculasm pel)
        {
            return db3.DeleteAsync(pel);
        }

        public Task<Peliculasm> GetPeliculaByIdAsync(int matricula)
        {
            return db3.Table<Peliculasm>().Where(a => a.matricula == matricula).FirstOrDefaultAsync();
        }
        ////////////////////////////////////////////
        ///Tabla generos
        public Task<int> GuardarGenero(Generosm gen)
        {
            if (gen.matricula != 0)
            {
                return db4.UpdateAsync(gen);
            }
            else
            {
                return db4.InsertAsync(gen);
            }
        }

        public Task<List<Generosm>> GetGeneros()
        {
            return db4.Table<Generosm>().ToListAsync();
        }

        public Task<int> BorrarGenero(Generosm gen)
        {
            return db4.DeleteAsync(gen);
        }

        public Task<Generosm> GetGeneroByIdAsync(int matricula)
        {
            return db4.Table<Generosm>().Where(a => a.matricula == matricula).FirstOrDefaultAsync();
        }

    }
}

