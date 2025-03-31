using MauiAppMinhasCompras.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public ListaProduto()
        {
            InitializeComponent();

            DataInicial = DateTime.Now.AddMonths(-1); // Último mês
            DataFinal = DateTime.Now;

            BindingContext = this;
            lst_produtos.ItemsSource = lista;
        }

        protected async override void OnAppearing()
        {
            await CarregarProdutos();
        }

        private async Task CarregarProdutos()
        {
            try
            {
                lista.Clear();
                List<Produto> produtos = await App.Db.GetAll();
                produtos.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new NovoProduto());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            double soma = lista.Sum(i => i.Total);
            DisplayAlert("Total dos Produtos", $"O total é {soma:C}", "OK");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = sender as MenuItem;
                Produto p = selecionado.BindingContext as Produto;
                bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");
                if (confirm)
                {
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Produto p = e.SelectedItem as Produto;
                await Navigation.PushAsync(new EditarProduto { BindingContext = p });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void lst_produtos_Refreshing(object sender, EventArgs e)
        {
            try
            {
                await CarregarProdutos();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }
        }

        private async void OnFiltrarClicked(object sender, EventArgs e)
        {
            try
            {
                List<Produto> produtosFiltrados = await App.Db.GetByDateRange(DataInicial, DataFinal);
                lista.Clear();
                produtosFiltrados.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void ToolbarItem_Clicked_Relatorio(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Relatorio());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                lst_produtos.ItemsSource = lista;
            }
            else
            {
                var filtered = lista.Where(p => p.Descricao.ToLower().Contains(e.NewTextValue.ToLower()));
                lst_produtos.ItemsSource = new ObservableCollection<Produto>(filtered);
            }
        }
    }
}
