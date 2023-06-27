using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Models
{
    public class ReservaClass
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime Horario { get; set; }
        public int AreaComumId { get; set; }
        public int MoradorId { get; set; }
        public AreaComum AreaComum { get; set; }
        public Morador Morador { get; set; }
    }
}
