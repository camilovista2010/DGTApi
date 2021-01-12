using DGT.Models.Vehiculo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Vehiculo
{
    public interface IVehiculoRepository
    {
        DtoVehiculo SetVehiculo(DtoVehiculo vehiculo); 
        DtoInfracciones SetInfracciones(DtoInfracciones infracciones);
        IEnumerable<DtoInfracciones> GetTopInfracciones();
    }
}
