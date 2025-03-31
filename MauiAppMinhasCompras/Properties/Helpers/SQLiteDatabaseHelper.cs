// SQLiteDatabaseHelper.cs
using MauiAppMinhasCompras.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) => _conn.InsertAsync(p);
        public Task<int> Delete(int id) => _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        public Task<int> Update(Produto p) => _conn.UpdateAsync(p);
        public Task<List<Produto>> GetAll() => _conn.Table<Produto>().ToListAsync();
        public Task<List<Produto>> GetByDateRange(DateTime dataInicial, DateTime dataFinal)
        {
            return _conn.Table<Produto>()
                .Where(p => p.DataCadastro >= dataInicial && p.DataCadastro <= dataFinal)
                .ToListAsync();
        }
    }
}
