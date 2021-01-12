using DGT.Data.Conductor;
using DGT.Models.Conductor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.BusinessLogic.Conductor
{
    public class ConductorBL : IConductorBL
    { 
        private readonly IConductorRepository repository;

        public ConductorBL(IConductorRepository repository) =>  this.repository = repository; 


        public object GetInfracciones(string DNI)
        {
            if(DNI.Length == 0)
                throw new ArgumentException("Debe Ingresar un valor de DNI");  

            return repository.GetInfracciones(DNI);

        }

        public IEnumerable<DtoConductor> GetTopConductores(int Cantidad)
        {
            if (Cantidad <= 0)
                throw new ArgumentException("Se debe ingresar un valor superior a 0");

            return repository.GetTopConductores(Cantidad);
        }

        public DtoConductor SetConductor(DtoConductor conductor)
        {
            if(conductor.DNI.Length == 0 || conductor.DNI == null)
                throw new ArgumentException("Debe Ingresar un valor de DNI");
            else if(conductor.Nombre.Length == 0 || conductor.Nombre == null)
                throw new ArgumentException("Nombre es requerido");
            else if (conductor.Apellidos.Length == 0 || conductor.Apellidos == null)
                throw new ArgumentException("Apellido es requerido");
            else if (conductor.Puntos == 0 || conductor.Puntos < 0 )
                throw new ArgumentException("Se debe ingresar un valor superior a 1");

            return repository.SetConductor(conductor);
        } 
      
    }
}
