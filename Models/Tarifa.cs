using System;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Models
{
    public class Tarifa
    {
        public int ID { get; set; }
        [DataType(DataType.Currency)] 
        [Display(Name = "Valor hora inicial")]
        public decimal VALHORAINI { get; set; }
        [DataType(DataType.Currency)] 
        [Display(Name = "Valor hora adicional")]
        public decimal VALHORAADI { get; set; }
        [Display(Name = "Inicio da vigência")]
        public DateTime DATAINIVIG { get; set; }
        [Display(Name = "Fim da vigência")]
        public DateTime DATAFIMVIG { get; set; }

    }
}