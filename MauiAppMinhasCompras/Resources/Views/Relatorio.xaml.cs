// Relatorio.xaml.cs
using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views
{
    public partial class Relatorio : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
        public DateTime DataInicial { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DataFinal { get; set; } = DateTime.Now;

        public Relatorio()
        {
            InitializeComponent();
            BindingContext = this;
            lst_produtos.ItemsSource = lista;
        }

        private async void Filtrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                lista.Clear();
                var produtos = await App.Db.GetByDateRange(DataInicial, DataFinal);
                produtos.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}
