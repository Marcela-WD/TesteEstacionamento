using System;
using System.ComponentModel.DataAnnotations;
namespace Estacionamento.Models
{
    public class Veiculo
    {
        public int ID { get; set; }
        public required string PLACA { get; set; }
        [Display(Name = "Hora de entrada")]
        public DateTime HORAENT { get; set; }
        [Display(Name = "Hora de saída")]
        public DateTime? HORASAI { get; set; }
    }
}