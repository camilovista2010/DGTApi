using DGT.Models.Vehiculo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.BusinessLogic.Vehiculo
{
    public interface IVehiculoBL
    {
        DtoVehiculo SetVehiculo(DtoVehiculo vehiculo);
        DtoInfracciones SetInfracciones(DtoInfracciones infracciones);
        IEnumerable<DtoInfracciones> GetTopInfracciones();
    }
}
