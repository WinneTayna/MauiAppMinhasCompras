using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{

    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }
    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();

        tmp.ForEach(i => lista.Add(i));
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var produto = menuItem?.BindingContext as Produto;

        if (produto != null)
        {
            bool confirm = await DisplayAlert("Confirmar", $"Deseja realmente remover {produto.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(produto.Id);
                lista.Remove(produto);
            }
        }
    }

    private async void ToolbarItem_Clicked_Remover(object sender, EventArgs e)
    {
        try
        {
            var produto = lst_produtos.SelectedItem as Produto;

            if (produto != null)
            {
                bool confirm = await DisplayAlert("Confirmar", $"Deseja realmente remover {produto.Descricao}?", "Sim", "Não");

                if (confirm)
                {
                    await App.Db.Delete(produto.Id);
                    lista.Remove(produto);
                }
            }
            else
            {
                await DisplayAlert("Erro", "Selecione um produto para remover.", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}