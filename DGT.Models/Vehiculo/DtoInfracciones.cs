using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Models.Vehiculo
{
    public class DtoInfracciones
    {
        public int Identificador { get; set; }
        public string Descripcion { get; set; }
        public int RestarPuntos { get; set; } 
        public string Matricula { get; set; }
        private DateTime Registro { get; set; } 


    }
}
