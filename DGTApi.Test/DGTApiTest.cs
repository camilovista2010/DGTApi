using Dapper;
using DGT.BusinessLogic.Conductor;
using DGT.BusinessLogic.Vehiculo;
using DGT.Data.Conductor;
using DGT.Data.Connection;
using DGT.Data.Vehiculo;
using DGT.Models.Base;
using DGT.Models.Conductor;
using DGT.Models.Vehiculo;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DGTApi.Test
{
    [TestFixture]
    public class DGTApiTest
    {
        private VehiculoBL _VehiculoBL;
        private ConductorBL _ConductorBL;
        private ConnectionFactory _Connection;
        public DGTApiTest()
        {
            _Connection = new ConnectionFactory();
            _Connection.InitDatabase();
        }

        [SetUp]
        public void Setup()
        {
            
            _VehiculoBL = new VehiculoBL(new VehiculoRepository(_Connection));
            _ConductorBL = new ConductorBL(new ConductorRepository(_Connection));
        }

        static IEnumerable<DtoConductor> Data1() => new DtoConductor[] { 
                new DtoConductor { DNI = "102078-87682", Nombre = "ESTEBAN" , Apellidos = "RUIZ" , Puntos = 5000 } }; 

        [Test, Order(1)]
        [TestCaseSource("Data1")]
        public void InsertarConductor_RetornarConductor(DtoConductor conductor)
        {
            //Añadir un nuevo conductor al sistema.
            var result = _ConductorBL.SetConductor(conductor);
            Assert.AreEqual(conductor, result);
        } 

        [Test, Order(2)]
        [TestCaseSource("Data1")]
        public void InsertarConductor_RetornarErrorIngresar(DtoConductor conductor)
        {
            //No puede haber dos conductores con el mismo DNI.  
            ArgumentException ex = Assert.Throws<ArgumentException>(
             delegate { _ConductorBL.SetConductor(conductor); });

            Assert.That(ex.Message, Is.EqualTo("El Conductor ya se encuentra registrado")); 
        }

        static IEnumerable<DtoVehiculo> Data2() => new DtoVehiculo[] { 
            new DtoVehiculo { DNI = "102078-87682", Marca = "AUTECO", Matricula = "KCM-64Y", Modelo = 2021 } }; 
       
        [Test, Order(3)]
        [TestCaseSource("Data2")]
        public void InsertarVehiculo_RetornarVehiculo(DtoVehiculo vehiculo)
        {
            //Añadir un nuevo vehículo al sistema.
            var result = _VehiculoBL.SetVehiculo(vehiculo);
            Assert.AreEqual(vehiculo, result);
        }


        [Test, Order(4)]
        [TestCaseSource("Data2")]
        public void InsertarVehiculoExistente_RetornarError(DtoVehiculo vehiculo)
        {
            //Si el vehículo ya existe devolver un ERROR.
            ArgumentException ex = Assert.Throws<ArgumentException>(
            delegate { _VehiculoBL.SetVehiculo(vehiculo); });

            Assert.That(ex.Message, Is.EqualTo("El Vehiculo ya se encuentra en el sistema"));

        } 

        static IEnumerable<DtoVehiculo> Data3() => new DtoVehiculo[] { new DtoVehiculo { DNI = "10255555", Marca = "UTECO", Matricula = "KCM-64D", Modelo = 2021 } };

        [Test, Order(5)]
        [TestCaseSource("Data3")]
        public void InsertarDNIExistente_RetornarError(DtoVehiculo vehiculo)
        {
            //Si DNI del conductor no existe devolver un ERROR.
            ArgumentException ex = Assert.Throws<ArgumentException>(
            delegate { _VehiculoBL.SetVehiculo(vehiculo); });

            Assert.That(ex.Message, Is.EqualTo("El Conductor no existe en el sistema, debe registrarlo."));

        }

        static IEnumerable<DtoVehiculo> Data4() => new DtoVehiculo[] 
        {   new DtoVehiculo {  DNI = "102078-87682", Marca = "AUTECO", Matricula = "PFT-64Y", Modelo = 2000 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "MAZDA", Matricula = "YFT-64Y", Modelo = 2021 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "BMW", Matricula = "HVF-64Y", Modelo = 2000 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "AUDI", Matricula = "KXM-64Y", Modelo = 2021 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "BENTLEY", Matricula = "MFE-64Y", Modelo = 2021 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "CHEVROLET", Matricula = "BVY-64Y", Modelo = 2000 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "CHEVROLET", Matricula = "OES-64Y", Modelo = 2021 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "FERRARI", Matricula = "NGH-64Y", Modelo = 2015 },
            new DtoVehiculo {  DNI = "102078-87682", Marca = "FERRARI", Matricula = "PWE-64Y", Modelo = 2021 },  };

        [Test, Order(6)]
        [TestCaseSource("Data4")]
        public void InsertarVehiculo_Max10(DtoVehiculo vehiculo)
        {
            //Puede haber más de un conductor habitual.
            var result = _VehiculoBL.SetVehiculo(vehiculo);
            Assert.AreEqual(vehiculo, result);

        }

        static IEnumerable<DtoVehiculo> Data6() => new DtoVehiculo[] {
            new DtoVehiculo { DNI = "102078-87682", Marca = "BMW", Matricula = "JIT-Y", Modelo = 2022 } };

        [Test, Order(7)]
        [TestCaseSource("Data6")]
        public void InsertarVehiculo_Max10Error(DtoVehiculo vehiculo)
        {
            //Puede haber más de un conductor habitual y el conductor en sí no puede ser habitual de más de 10 vehículos.
            ArgumentException ex = Assert.Throws<ArgumentException>(
            delegate { _VehiculoBL.SetVehiculo(vehiculo); });
             
            Assert.That(ex.Message, Is.EqualTo("El Conductor no puede superar los 10 vehiculos"));

        }


        static IEnumerable<DtoInfracciones> Data7() => new DtoInfracciones[] {
            new DtoInfracciones {  Identificador = 100 ,  Descripcion = "SOBRE PASAR  SEMAFORO"  , RestarPuntos = 80 , Matricula = "PFT-64Y"  },  };

        [Test, Order(8)]
        [TestCaseSource("Data7")]
        public void InsertarInfracion_RetornarInfracion(DtoInfracciones infraccion)
        {
            //Añadir infracciones al sistema
            var result = _VehiculoBL.SetInfracciones(infraccion);
            Assert.AreEqual(infraccion, result);
        }


        static IEnumerable<DtoInfracciones> Data8() => new DtoInfracciones[] {
             new DtoInfracciones {  Identificador = 101 , Descripcion = "AUMENTO VELOCIDAD" , Matricula = "PFT-64Y", RestarPuntos = 100  },
            new DtoInfracciones { Identificador = 102 , Descripcion = "AUMENTO VELOCIDAD" , Matricula = "YFT-64Y", RestarPuntos = 100 },
            new DtoInfracciones {  Identificador = 103 , Descripcion = "SOBRE PASAR  SEMAFORO ROJO" , Matricula = "HVF-64Y", RestarPuntos = 80  },
            new DtoInfracciones { Identificador = 104 , Descripcion = "PARQUEA LUGAR PROHIBIDO" , Matricula = "KXM-64Y", RestarPuntos = 20  },
            new DtoInfracciones { Identificador = 105 , Descripcion = "MANEJO SUPERIOR A 40KM/H" , Matricula = "MFE-64Y",RestarPuntos = 10  },
            new DtoInfracciones {  Identificador = 106 , Descripcion = "SOBRE PASAR  SEMAFORO" , Matricula = "BVY-64Y", RestarPuntos = 80  },
            new DtoInfracciones {  Identificador = 107 , Descripcion = "SOBRE PASAR  SEMAFORO AMARILLO" , Matricula = "OES-64Y", RestarPuntos = 30  },
            new DtoInfracciones {  Identificador = 108 , Descripcion = "SOBRE PASAR  SEMAFORO" , Matricula = "NGH-64Y", RestarPuntos = 80 },
            new DtoInfracciones {  Identificador = 109 , Descripcion = "SOBRE PASAR  SEMAFORO" , Matricula = "PWE-64Y",  RestarPuntos = 80  },
            new DtoInfracciones {  Identificador = 110 ,  Descripcion = "SOBRE PASAR  SEMAFORO ROJO"  ,  Matricula = "PFT-64Y" , RestarPuntos = 80  },  };

        [Test, Order(9)]
        [TestCaseSource("Data8")]
        public void InsertarInfracion_PorVehiculo(DtoInfracciones infraccion)
        {
            //Registrar una infracción por vehículo.
            var result = _VehiculoBL.SetInfracciones(infraccion);
            Assert.AreEqual(infraccion, result);
        }


        //Consultar historial de infracciones de un conductor.


        [Test, Order(10)]
        [TestCase("102078-87682")]
        public void ObtenerInfraciones_PorDNI(string DNI)
        {
            //Registrar una infracción por vehículo.
            var result = _ConductorBL.GetInfracciones(DNI);
            Assert.IsNotNull(result); 
        } 

        [Test, Order(11)]
        public void ObtenerTopInfraciones()
        {
            //Consultar los 5 tipos de infracción más habituales.
            var result  = _VehiculoBL.GetTopInfracciones();
            Assert.IsTrue(result.ToList().Count <= 5 );
        }


        [Test, Order(12)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(5)]
        public void ObtenerTopConductores(int Cantidad)
        {
            //Consultar el top N de conductores.
            var result = _ConductorBL.GetTopConductores(Cantidad);
            Assert.IsTrue(result.ToList().Count <= Cantidad);
        }


    }
}
