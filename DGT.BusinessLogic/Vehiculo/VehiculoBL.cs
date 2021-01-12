using DGT.Data.Vehiculo;
using DGT.Models.Vehiculo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.BusinessLogic.Vehiculo
{
    public class VehiculoBL : IVehiculoBL
    {
        private readonly IVehiculoRepository repository;

        public VehiculoBL(IVehiculoRepository repository)
        {
            this.repository = repository;
        }
         

        public IEnumerable<DtoInfracciones> GetTopInfracciones()
        {
            return this.repository.GetTopInfracciones();
        }

        public DtoInfracciones SetInfracciones(DtoInfracciones infracciones)
        {
            if (infracciones.Matricula.Length == 0 || infracciones.Matricula == null)
                throw new ArgumentException("Debe Ingresar un valor de Matricula");
            else if (infracciones.Descripcion.Length == 0 || infracciones.Descripcion == null)
                throw new ArgumentException("Descripción es requerido");
            else if (infracciones.Identificador == 0 )
                throw new ArgumentException("Identificador es requerido");
            else if (infracciones.RestarPuntos == 0 || infracciones.RestarPuntos < 0)
                throw new ArgumentException("Cammpo RestarPunto, se debe ingresar un valor mayor a 1");

            return this.repository.SetInfracciones(infracciones);
        }

        public DtoVehiculo SetVehiculo(DtoVehiculo vehiculo)
        {
            if (vehiculo.DNI.Length == 0 || vehiculo.DNI == null)
                throw new ArgumentException("Debe Ingresar un valor de Matricula");
            else if (vehiculo.Marca.Length == 0 || vehiculo.Marca == null)
                throw new ArgumentException("Descripción es requerido");
            else if (vehiculo.Matricula.Length == 0 || vehiculo.Matricula == null)
                throw new ArgumentException("Descripción es requerido");
            else if (vehiculo.Modelo <= 0)
                throw new ArgumentException("Cammpo RestarPunto, se debe ingresar un valor mayor a 1"); 

            return this.repository.SetVehiculo(vehiculo);
        }
    }
}
