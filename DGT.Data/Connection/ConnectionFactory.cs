using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace DGT.Data.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IDbConnection _connection;
        private string DbConnectionString {  get => "Data Source=DGT.sqlite"; }

        public ConnectionFactory()
        {  

        }

        public IDbConnection GetConnection
        {
            get
            {
                _connection = new SqliteConnection(DbConnectionString);
                return _connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                try
                {
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
       }

        public void InitDatabase()
        { 
             
            SqlMapper.Execute(GetConnection, @"DROP TABLE IF EXISTS 'Conductor';
                CREATE TABLE 'Conductor' ( 
                  'DNI' TEXT,
                  'Nombre' TEXT(255),
                  'Apellidos' TEXT(255),
                  'Puntos' integer,
                  PRIMARY KEY ('DNI')
                );");

            SqlMapper.Execute(GetConnection, @"DROP TABLE IF EXISTS 'Vehiculo';
                CREATE TABLE 'Vehiculo' (
                  'Matricula' TEXT NOT NULL,
                  'Marca' TEXT,
                  'Modelo' integer, 
                  'DNI' TEXT,
                  'Registro' DATETIME DEFAULT CURRENT_TIMESTAMP ,
                  PRIMARY KEY ('Matricula')
                );");

            SqlMapper.Execute(GetConnection, @"DROP TABLE IF EXISTS 'Infracciones';
                CREATE TABLE 'Infracciones' (
                  'Identificador' integer, 
                  'Descripcion' TEXT NOT NULL,
                  'RestarPuntos' integer,  
                  'Matricula' TEXT  NOT NULL,
                  'Registro' DATETIME DEFAULT CURRENT_TIMESTAMP ,
                  PRIMARY KEY ('Identificador')
                );");
        }
    }
}
