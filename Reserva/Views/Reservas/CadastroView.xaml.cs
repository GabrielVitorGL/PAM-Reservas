using Reserva.ViewModels.Reservas;

namespace Reserva.Views.Reservas;

public partial class CadastroView : ContentPage
{

    private CadastroReservaViewModel cadViewModel;
    public CadastroView()
    {
        InitializeComponent();

        cadViewModel = new CadastroReservaViewModel();
        BindingContext = cadViewModel;
        Title = "Nova Reserva";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}