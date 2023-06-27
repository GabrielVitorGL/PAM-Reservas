using Reserva.Models;
using Reserva.Services.Reservas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reserva.ViewModels.Reservas
{
    public class ListagemReservaViewModel : BaseViewModel
    {
        private ReservaService pService;

        public ObservableCollection<ReservaClass> Reservas { get; set; }

        public ListagemReservaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new ReservaService(token);
            Reservas = new ObservableCollection<ReservaClass>();

            _ = ObterReservas();

            NovaReserva = new Command(async () => { await ExibirCadastroReserva(); });
            RemoverReservaCommand = new Command<ReservaClass>(async (ReservaClass r) => { await RemoverReserva(r); });
        }

        public ICommand NovaReserva { get; }
        public ICommand RemoverReservaCommand { get; }

        public async Task ObterReservas()
        {
            try
            {
                Reservas = await pService.GetReservasAsync();
                OnPropertyChanged(nameof(Reservas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task ExibirCadastroReserva()
        {
            try
            {
                await Shell.Current.GoToAsync("cadReservaView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        private ReservaClass reservaSelecionada;

        public ReservaClass ReservaSelecionada
        {
            get { return reservaSelecionada; }
            set
            {
                if (value != null)
                {
                    reservaSelecionada = value;

                    Shell.Current
                    .GoToAsync($"cadReservaView?pId={reservaSelecionada.Id}");
                }
            }
        }

        public async Task RemoverReserva(ReservaClass r)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Confirmação", $"Confirma que quer remover a reserva de {r.AreaComum.Nome}?", "Sim", "Não"))
                {
                    await pService.DeleteReservaAsync(r.Id);


                }
            }
            catch {}
            await Application.Current.MainPage.DisplayAlert("Mensagem", "Reserva removida com sucesso!", "Ok");
            _ = ObterReservas();
        }
    }
}
