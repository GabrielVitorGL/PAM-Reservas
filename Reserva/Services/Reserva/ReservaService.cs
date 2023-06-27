using AppRpgEtec.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reserva.Models;

namespace Reserva.Services.Reservas
{
    public class ReservaService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://www.onerb.somee.com/pam/Reserva";
        //xyz seu site

        private string _token;

        public ReservaService(string token)
        {
            _request = new Request();
            _token = token;
        }
        public async Task<ObservableCollection<ReservaClass>> GetReservasAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.ReservaClass> listaReservas = await
            _request.GetAsync<ObservableCollection<Models.ReservaClass>>(apiUrlBase + urlComplementar, _token);
            return listaReservas;
        }
        public async Task<ReservaClass> GetReservaAsync(int reservaId)
        {
            string urlComplementar = string.Format("/{0}", reservaId);
            var reserva = await _request.GetAsync<Models.ReservaClass>(apiUrlBase + urlComplementar, _token);
            return reserva;
        }
        public async Task<int> PostReservaAsync(ReservaClass p)
        {
            return await _request.PostReturnIntTokenAsync(apiUrlBase, p, _token);
        }
        public async Task<int> PutReservaAsync(ReservaClass p)
        {
            var result = await _request.PutAsync(apiUrlBase, p, _token);
            return result;
        }
        public async Task<int> DeleteReservaAsync(int reservaId)
        {
            string urlComplementar = string.Format("/{0}", reservaId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
