using MauiAppMinhasCompras.Helpers;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;

        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }

                return _db;
            }
        }

        public App()
        {
            InitializeComponent();

            // Define a cultura como "pt-BR" para formatação de datas e números
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            // Define a página principal utilizando NavigationPage para permitir a navegação entre telas
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
