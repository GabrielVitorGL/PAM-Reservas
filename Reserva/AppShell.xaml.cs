using Reserva.Views.Reservas;

namespace Reserva;
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("cadReservaView", typeof(CadastroView));
	}
}
