using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Navegacion.Modelos;
using Navegacion.BASEDATOS;
using System.IO;

namespace Navegacion
{
    public partial class App : Application
    {
        static SQLiteOperaciones db;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
        public static SQLiteOperaciones SQLiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteOperaciones(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "baseUPVM.db3"));
                }
                return db;
            }
        }
        public static SQLiteOperaciones SQLite
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteOperaciones(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "baseUPVM.db3"));
                }
                return db;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
