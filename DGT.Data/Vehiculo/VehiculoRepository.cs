using Dapper;
using DGT.Data.Connection;
using DGT.Models.Vehiculo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace DGT.Data.Vehiculo
{
    public class VehiculoRepository : IVehiculoRepository
    {
        public readonly IConnectionFactory _conn;
        public VehiculoRepository(IConnectionFactory connectionFactory)
        {
            _conn = connectionFactory;
        }

        public IEnumerable<DtoInfracciones> GetTopInfracciones()
        {
            IEnumerable<DtoInfracciones> lst = new List<DtoInfracciones>();
            try
            {
                lst = SqlMapper.Query<DtoInfracciones>(_conn.GetConnection, @" SELECT count(b.Identificador) as Cantidad, b.Identificador, b.Descripcion, b.RestarPuntos FROM Vehiculo as a INNER JOIN Infracciones as b ON a.Matricula = b.Matricula   GROUP BY b.Descripcion ORDER BY count(b.Identificador) DESC LIMIT 5;", null, commandType: CommandType.Text);
            }
            finally
            {
                _conn.CloseConnection();
            }

            return lst;
        }

        public DtoInfracciones SetInfracciones(DtoInfracciones infracciones)
        {
            try
            {
                int _cant = 0;
                string _DNI = MatriculaConductor(infracciones.Matricula);

                if (_DNI == null)
                    throw new ArgumentException("No existe conductores asignados a la matricula digitada.");
                if (ExisteVehiculo(infracciones.Matricula) == false)
                    throw new ArgumentException("El Vehiculo no existe en el sistema");
                else
                {
                    _cant = SqlMapper.Execute(_conn.GetConnection, @"INSERT INTO 'Infracciones' ('Identificador', 'Descripcion', 'RestarPuntos', 'Matricula') VALUES (@Identificador, @Descripcion, @RestarPuntos, @Matricula)", infracciones, commandType: CommandType.Text);
                    _cant = _cant > 0 ? DisminuirPuntos(_DNI, infracciones.RestarPuntos) : 0;
                }


                if (_cant == 0)
                    throw new ArgumentException("Error al ingresar los datos");

            }
            finally
            {
                _conn.CloseConnection();
            }

            return infracciones;
        }

        public DtoVehiculo SetVehiculo(DtoVehiculo vehiculo)
        {
            try
            {
                int _cant = 0;
                int CantidadHabitual = 10;

                if (ExisteConductor(vehiculo.DNI) == null)
                    throw new ArgumentException("El Conductor no existe en el sistema, debe registrarlo con el dni :" + vehiculo.DNI);
                if (ExisteVehiculo(vehiculo.Matricula) == true)
                    throw new ArgumentException("El Vehiculo ya se encuentra en el sistema");
                if (ConductorHabitual(vehiculo.DNI) >= CantidadHabitual)
                    throw new ArgumentException("El Conductor no puede superar los 10 vehiculos");
                else
                    _cant = SqlMapper.Execute(_conn.GetConnection, @"INSERT INTO 'Vehiculo'('Matricula', 'Marca', 'Modelo', 'DNI') VALUES (@Matricula, @Marca, @Modelo, @DNI);", vehiculo, commandType: CommandType.Text);

                if (_cant == 0)
                    throw new ArgumentException("Error al ingresar los datos");

            }
            finally
            {
                _conn.CloseConnection();
            }

            return vehiculo;
        }


        private bool ExisteVehiculo(string Matricula)
        {
            return SqlMapper.QuerySingle<int>(_conn.GetConnection, @"select count(Matricula) from Vehiculo where Matricula = @Matricula;", new { Matricula }) > 0 ? true : false;
        }

        private string ExisteConductor(string DNI)
        {
            return SqlMapper.QueryFirstOrDefault<string>(_conn.GetConnection, @"select DNI from Conductor where DNI = @DNI ;", new { DNI });
        }

        private string MatriculaConductor(string Matricula)
        {
            return SqlMapper.QueryFirstOrDefault<string>(_conn.GetConnection, @"SELECT a.DNI FROM Conductor AS a INNER JOIN Vehiculo AS b ON a.DNI = b.DNI WHERE b.Matricula = @Matricula GROUP BY a.DNI ;", new { Matricula });
        }


        private int ConductorHabitual(string DNI)
        {
            return SqlMapper.QuerySingle<int>(_conn.GetConnection, @"SELECT Count(b.Matricula) FROM Conductor AS a INNER JOIN Vehiculo AS b ON a.DNI = b.DNI WHERE a.DNI = @DNI;", new { DNI });
        }

        private int DisminuirPuntos(string DNI , int Puntos)
        {
           int _Puntos = SqlMapper.QuerySingle<int>(_conn.GetConnection, @"SELECT Puntos FROM  'Conductor' WHERE DNI = @DNI;", new { DNI });

            return SqlMapper.Execute(_conn.GetConnection, @"UPDATE 'Conductor' SET 'Puntos' = @Puntos  WHERE DNI = @DNI;", new { Puntos = _Puntos - Puntos , DNI });
        }
    }
}
