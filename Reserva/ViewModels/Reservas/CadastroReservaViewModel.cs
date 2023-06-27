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
    [QueryProperty("ReservaSelecionadaId", "pId")]
    public class CadastroReservaViewModel : BaseViewModel
    {
        private ReservaService reservaService;

        public CadastroReservaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            reservaService = new ReservaService(token);

            SalvarCommand = new Command(async () => await SalvarReserva());
            CancelarCommand = new Command(async () => await CancelarReserva());
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private DateTime data;
        public DateTime Data
        {
            get => data;
            set
            {
                data = value;
                OnPropertyChanged();
            }
        }


        private int moradorId;
        public int MoradorId
        {
            get => moradorId;
            set
            {
                moradorId = value;
                OnPropertyChanged();
            }
        }

        private int areaComumId;
        public int AreaComumId
        {
            get => areaComumId;
            set
            {
                areaComumId = value;
                OnPropertyChanged();
            }
        }



        public async Task SalvarReserva()
        {
            try
            {
                ReservaClass reserva = new ReservaClass()
                {
                    Id = this.Id,
                    MoradorId = this.MoradorId,
                    AreaComumId = this.AreaComumId,
                    Data = this.Data

                };

                if (reserva.Id == 0)
                    await reservaService.PostReservaAsync(reserva);
                else
                    await reservaService.PutReservaAsync(reserva);


            }
            catch {}

            await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

            await Shell.Current.GoToAsync(".."); //Remove the current page from the page stack
        }

        public async void CarregarReserva()
        {
            try
            {
                ReservaClass r = await reservaService.GetReservaAsync(int.Parse(reservaSelecionadaId));
                this.AreaComumId = r.AreaComum.Id;
                this.MoradorId = r.Morador.Id;
                this.Id = r.Id;
                this.Data = r.Data;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private string reservaSelecionadaId;
        public string ReservaSelecionadaId
        {
            set
            {
                if (value != null)
                {
                    reservaSelecionadaId = Uri.UnescapeDataString(value);
                    CarregarReserva();
                }
            }
        }

        private async Task CancelarReserva()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}