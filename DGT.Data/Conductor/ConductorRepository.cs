using Dapper;
using DGT.Data.Connection;
using DGT.Models.Conductor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DGT.Data.Conductor
{
    public class ConductorRepository : IConductorRepository
    {

        public readonly IConnectionFactory _conn; 
        public ConductorRepository(IConnectionFactory connectionFactory)
        {
            _conn = connectionFactory;
        }

        public object GetInfracciones(string DNI)
        { 

           IEnumerable<dynamic> _lst = new List<dynamic>();
            try
            {
                _lst = SqlMapper.Query(_conn.GetConnection, @"SELECT a.DNI, a.Nombre, a.Apellidos, a.Puntos, b.Matricula, b.Marca, b.Modelo, c.Identificador, c.Descripcion, c.RestarPuntos, c.Registro  FROM Conductor AS a INNER JOIN Vehiculo AS b ON a.DNI = b.DNI INNER JOIN Infracciones AS c ON b.Matricula = c.Matricula WHERE a.DNI = @DNI;", new { DNI }, commandType: CommandType.Text);
            }
            finally
            {
                _conn.CloseConnection();
            }

            return _lst;
        }

        public IEnumerable<DtoConductor> GetTopConductores(int Cantidad)
        {
            IEnumerable<DtoConductor> _lst = new List<DtoConductor>();
            try
            {
                _lst = SqlMapper.Query<DtoConductor>(_conn.GetConnection, @"SELECT * FROM Conductor  ORDER BY ROWID ASC LIMIT @Cantidad ;", new { Cantidad }, commandType: CommandType.Text);
            }
            finally
            {
                _conn.CloseConnection();
            }

            return _lst;
        }

        public DtoConductor SetConductor(DtoConductor conductor)
        {
            try
            {
                int _cant = 0;

                if (ExisteConductor(conductor.DNI) == true)
                    throw new ArgumentException("El Conductor ya se encuentra registrado"); 
                else
                    _cant = SqlMapper.Execute(_conn.GetConnection, @"INSERT INTO 'Conductor' ('DNI', 'Nombre', 'Apellidos', 'Puntos') VALUES (@DNI, @Nombre, @Apellidos, @Puntos);", conductor , commandType: CommandType.Text);

                if (_cant == 0)
                    throw new ArgumentException("Error al ingresar los datos");

            }
            finally
            {
                _conn.CloseConnection();
            }

            return conductor;
        }


        private bool ExisteConductor(string DNI)
        {
            return SqlMapper.QuerySingle<int>(_conn.GetConnection, @"select count(DNI) from Conductor where DNI = @DNI ;", new { DNI }) > 0 ? true : false;
        }
    }
}
