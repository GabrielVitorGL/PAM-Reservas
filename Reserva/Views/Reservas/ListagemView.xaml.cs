using Reserva.ViewModels.Reservas;

namespace Reserva.Views.Reservas;

public partial class ListagemView : ContentPage
{
    ListagemReservaViewModel viewModel;

    public ListagemView()
	{
		InitializeComponent();

        viewModel = new ListagemReservaViewModel();
        BindingContext = viewModel;
        Title = "Reservas";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.ObterReservas();
    }
}