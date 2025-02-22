using MauiAppMinhasCompras.Resources.Views;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

        }

        protected override Window CreateWindow(IActivationState? activationState) // A interrogação(?) significa que marquei como anulável, o parâmetro activationState.
        {
            return new Window(new NavigationPage(new ListaProduto())); // Usando a classe ListaProduto
        }
    }
}

