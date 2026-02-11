using System;

namespace Estacionamento.Models
{
    public class Veiculo
    {
        public int ID { get; set; }
        public required string PLACA { get; set; }
        public DateTime HORAENT { get; set; }
        public DateTime? HORASAI { get; set; }
    }
}