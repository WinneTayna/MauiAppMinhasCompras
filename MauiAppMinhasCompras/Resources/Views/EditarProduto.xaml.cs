using ProdutoModel = MauiAppMinhasCompras.Models.Produto;

namespace MauiAppMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                ProdutoModel produto_anexado = BindingContext as ProdutoModel;

                ProdutoModel p = new ProdutoModel
                {
                    Id = produto_anexado.Id,
                    Descricao = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_quantidade.Text),
                    Preco = Convert.ToDouble(txt_preco.Text)
                };

                await App.Db.Update(p);
                await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
