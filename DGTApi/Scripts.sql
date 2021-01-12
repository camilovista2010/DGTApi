DROP TABLE IF EXISTS "Conductor";
CREATE TABLE "Conductor" ( 
  "DNI" TEXT,
  "Nombre" TEXT(255),
  "Apellidos" TEXT(255),
  "Puntos" integer,
  PRIMARY KEY ("DNI")
);

DROP TABLE IF EXISTS "Vehiculo";
CREATE TABLE "Vehiculo" (
  "Matricula" TEXT NOT NULL,
  "Marca" TEXT,
  "Modelo" integer, 
  "DNI" TEXT,
  "Registro" DATETIME DEFAULT CURRENT_TIMESTAMP ,
  PRIMARY KEY ("Matricula")
);

DROP TABLE IF EXISTS "Infracciones";
CREATE TABLE "Infracciones" (
  "Identificador" TEXT NOT NULL,
  "Descripcion" TEXT NOT NULL,
  "RestarPuntos" integer,  
  "Matricula" TEXT  NOT NULL,
  "Registro" DATETIME DEFAULT CURRENT_TIMESTAMP ,
  PRIMARY KEY ("Identificador")
);

 