using DGT.Models.Conductor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Data.Conductor
{
    public interface IConductorRepository
    {
         DtoConductor SetConductor(DtoConductor conductor); 
         Object GetInfracciones(string DNI); 
         IEnumerable<DtoConductor> GetTopConductores(int Cantidad);
    }
}
