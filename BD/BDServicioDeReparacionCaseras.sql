use [master]
go
create database ServiDeReparCaseros
go
use ServiDeReparCaseros
go
create table Cargo(
CodigoCargo int primary key identity(1,1) not null,
Nombre varchar(50) not null,
Salario money not null,
)
go
create table Departamento(
CodigoDepartamento int primary key identity(1,1) not null,
Nombre varchar(50) not null,
Estado bit not null
)
go 
create table Ciudad(
CodigoCiudad int primary key identity(1,1) not null,
Nombre varchar(50) not null,
Estado bit not null,
CodigoDepartamento int not null,
foreign key (CodigoDepartamento) references Departamento(CodigoDepartamento)
)
go
create table Sede(
IdSede int primary key identity(1,1) not null,
Nombre varchar(50) not null,
Direccion varchar(50) unique not null,
Telefono varchar(20) not null,
CodigoCiudad int not null,
foreign key (CodigoCiudad) references Ciudad(CodigoCiudad)
)
go
create table TipoContrato(
IdTipoContrato int primary key identity(1,1) not null,
Nombre varchar(50) not null
)
go
create table Empleado(
CCEmpleado varchar(20) primary key not null,
NombreCompleto varchar(70) not null,
FechaNacimiento date null,
Telefono varchar(20) not null,
Direccion varchar(200) not null,
IdTipoContrato int not null,
foreign key (IdTipoContrato) references TipoContrato(IdTipoContrato)
)
go
create table EmpleadoCargo(
CodigoEmpleCargo int primary key identity(1,1) not null,
FechaInicio datetime not null,
FechaFin datetime null,
CodigoCargo int not null,
CCEmpleado varchar(20) not null,
IdSede int not null,
foreign key (CodigoCargo) references Cargo(CodigoCargo),
foreign key (CCEmpleado) references Empleado(CCEmpleado),
foreign key (IdSede) references Sede(IdSede)
)
go
create table FotosEmpleado(
IdFoto int primary key identity(1,1) not null,
NombreFoto varchar(50) not null,
CCEmpleado varchar(20) not null,
foreign key (CCEmpleado) references Empleado(CCEmpleado)
)
go
create table Usuario(
IdUsuario int primary key identity(1,1) not null,
CCEmpleado varchar(20) null,
UserName varchar(50) not null,
Clave nvarchar(2000) not null,
Salt nvarchar(2000) null,
foreign key (CCEmpleado) references Empleado(CCEmpleado)
)
go
create table Perfil(
IdPerfil int primary key identity(1,1) not null,
Nombre varchar(50) not null,
PaginaNavegar varchar(50) not null
)
go
create table PerfilUsuario(
IdPerfilUsuario int primary key identity(1,1) not null,
IdUsuario int not null,
IdPerfil int not null,
Estado bit not null,
foreign key (IdUsuario) references Usuario(IdUsuario),
foreign key (IdPerfil) references Perfil(IdPerfil)
)
go
create table Servicio(
CodigoServicio int primary key identity(1,1) not null,
Nombre varchar(50) not null,
Descripcion text null,
Precio money not null
)
go 
create table Cliente(
Documento varchar(20) primary key not null,
NombreCompleto varchar(70) not null,
FechaNacimiento date null,
Direccion varchar(200) not null,
Correo varchar(200) null
)
go
create table Programacion(
IdProgramacion int primary key identity(1,1) not null,
Fecha date not null,
Hora time not null,
Direccion varchar(200) not null,
Estado varchar(50) not null check (Estado in ('agendada', 'asignada', 'en camino', 'cumplida', 'registrada')),
Documento varchar(20) not null,
CodigoServicio int not null,
foreign key (Documento) references Cliente(Documento),
foreign key (CodigoServicio) references Servicio(CodigoServicio)
)
go
create table ProgramacionEmpleado(
CCEmpleado varchar(20) not null,
IdProgramacion int not null,
primary key (CCEmpleado, IdProgramacion),
foreign key (CCEmpleado) references Empleado(CCEmpleado),
foreign key (IdProgramacion) references Programacion(IdProgramacion)
)
go
create table ProgramacionServicio(
CodigoServicio int not null,
IdProgramacion int not null,
primary key (CodigoServicio, IdProgramacion),
foreign key (CodigoServicio) references Servicio(CodigoServicio),
foreign key (IdProgramacion) references Programacion(IdProgramacion)
)
go
create table Factura(
Numero int primary key not null,
Documento varchar(20) not null,
Fecha datetime not null,
CodigoEmpleCargo int not null,
foreign key (Documento) references Cliente(Documento),
foreign key (CodigoEmpleCargo) references EmpleadoCargo(CodigoEmpleCargo)
)
go
create table DetalleFactura(
Codigo int primary key identity(1,1) not null,
Numero int not null,
CodigoServicio int not null,
Cantidad int not null,
Subtotal money not null,
foreign key (Numero) references Factura(Numero),
foreign key (CodigoServicio) references Servicio(CodigoServicio)
)
go
create table Telefono(
Codigo int primary key identity(1,1) not null,
Numero varchar(20) not null,
Documento varchar(20) not null,
foreign key (Documento) references Cliente(Documento)
)
go
create table Proveedor(
IdProveedor varchar(20) primary key not null,
Nombre varchar(50) not null,
RazonSocial varchar(200) not null,
Direccion varchar(200) not null,
Telefono varchar(20) not null,
SitioWeb varchar(200) null
)
go 
create table ContactoProveedor(
CodigoProvee int primary key identity(1,1) not null,
NombreCompleto varchar(70) not null,
Telefono varchar(20) not null,
Cargo varchar(50) not null,
IdProveedor varchar(20) not null,
foreign key (IdProveedor) references Proveedor(IdProveedor)
)
go
create table Equipo(
CodigoEquipo int primary key not null,
Nombre varchar(50) not null,
Descripcion text not null,
Cantidad int not null
)
go 
create table ImagenesEquipo(
IdImagen int primary key identity(1,1) not null,
NombreImagen varchar(50) not null,
CodigoEquipo int not null,
foreign key (CodigoEquipo) references Equipo(CodigoEquipo)
)
go
create table EquipoProgramacion(
Codigo int primary key identity(1,1) not null,
CodigoEquipo int not null,
IdProgramacion int not null,
Cantidad int not null,
foreign key (CodigoEquipo) references Equipo(CodigoEquipo),
foreign key (IdProgramacion) references Programacion(IdProgramacion)
)
go
create table EquipoProveedor(
Codigo int primary key identity(1,1) not null,
IdProveedor varchar(20) not null,
CodigoEquipo int not null,
ValorUnitario money not null,
FechaCotizacion datetime not null,
FechaValidez datetime not null,
foreign key (IdProveedor) references Proveedor(IdProveedor),
foreign key (CodigoEquipo) references Equipo(CodigoEquipo)
)
go
create table FacturaCompra(
NumeroFactura int primary key not null,
CodigoEmpleCargo int not null,
IdProveedor varchar(20) not null,
FechaCompra datetime not null,
FechaPago datetime not null,
foreign key (CodigoEmpleCargo) references EmpleadoCargo(CodigoEmpleCargo),
foreign key (IdProveedor) references Proveedor(IdProveedor) 
)
go
create table DetalleFacturaCompra(
Codigo int primary key identity(1,1) not null,
NumeroFactura int not null,
CodigoEquipo int not null,
Cantidad int not null,
ValorUnitario money not null,
foreign key (NumeroFactura) references FacturaCompra(NumeroFactura),
foreign key (CodigoEquipo) references Equipo(CodigoEquipo)
)
go
INSERT Cargo (Nombre, Salario) VALUES ('Técnico de Reparaciones', 35000);
INSERT Cargo (Nombre, Salario) VALUES ('Supervisor de Técnicos', 45000);
INSERT Cargo (Nombre, Salario) VALUES ('Coordinador de Servicios', 40000);
INSERT Cargo (Nombre, Salario) VALUES ('Asistente Administrativo', 22000);
INSERT Cargo (Nombre, Salario) VALUES ('Recepcionista', 18000);
INSERT Cargo (Nombre, Salario) VALUES ('Gerente de Operaciones', 60000);
go
INSERT Departamento (Nombre, Estado) VALUES (N'ANTIOQUIA', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'VALLE', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'BOLIVAR', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'CALDAS', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'FLORIDA', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'CALIFORNIA', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'TEXAS', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'ORELLANA', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'ESMERALDAS', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'TACHIRA', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'VISCAYA', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'TOLEDO', 1)
INSERT Departamento (Nombre, Estado) VALUES (N'GRANADA', 1)
go
INSERT Ciudad (Nombre, Estado, CodigoDepartamento) VALUES (N'MEDELLIN', 1, 1)
go
INSERT INTO Cliente (Documento, NombreCompleto, FechaNacimiento, Direccion, Correo) VALUES 
('1002456789', 'Carlos Pérez Gómez', '1985-06-15', 'Calle 50 # 12-34, Medellín, Antioquia', 'carlos.perez@gmail.com'),
('1009876543', 'Ana María López', '1992-08-23', 'Carrera 80 # 15-67, Envigado, Antioquia', 'ana.lopez@hotmail.com'),
('1023456789', 'Luis Alberto Martínez', '1979-11-05', 'Calle 10 # 45-23, Rionegro, Antioquia', 'luis.martinez@yahoo.com'),
('1034567890', 'María Fernanda Rodríguez', '1990-02-10', 'Carrera 50 # 8-12, Itagüí, Antioquia', 'maria.rodriguez@outlook.com'),
('1045678901', 'José Andrés García', '1983-01-30', 'Avenida Las Vegas # 32-21, Medellín, Antioquia', 'jose.garcia@gmail.com'),
('1056789012', 'Carolina Gómez Ríos', '1988-07-17', 'Calle 80 # 10-19, Bello, Antioquia', 'carolina.gomez@hotmail.com'),
('1067890123', 'Pedro Alejandro Sánchez', '1995-12-03', 'Carrera 70 # 5-45, La Ceja, Antioquia', 'pedro.sanchez@live.com'),
('1078901234', 'Verónica Ruiz Pérez', '1980-09-11', 'Calle 12 # 33-54, Sabaneta, Antioquia', 'veronica.ruiz@gmail.com'),
('1089012345', 'Ricardo Gómez Martínez', '1993-04-19', 'Calle 45 # 20-67, Envigado, Antioquia', 'ricardo.gomez@yahoo.com'),
('1090123456', 'Laura Isabel Castillo', '1997-10-27', 'Carrera 35 # 18-26, Apartadó, Antioquia', 'laura.castillo@hotmail.com');
go
INSERT INTO TipoContrato (Nombre) VALUES ('Contrato a término fijo');
INSERT INTO TipoContrato (Nombre) VALUES ('Contrato a término indefinido');
INSERT INTO TipoContrato (Nombre) VALUES ('Contrato por obra o labor');
INSERT INTO TipoContrato (Nombre) VALUES ('Contrato por horas');
INSERT INTO TipoContrato (Nombre) VALUES ('Contrato de aprendizaje');
INSERT INTO TipoContrato (Nombre) VALUES ('Contrato de prestación de servicios');
go
INSERT INTO Empleado (CCEmpleado, NombreCompleto, FechaNacimiento, Telefono, Direccion, IdTipoContrato) VALUES
('1001234567', 'Carlos Alberto Díaz', '1985-03-10', '3001234567', 'Calle 45 # 23-11, Medellín, Antioquia', 1),
('1002345678', 'Ana María González', '1990-07-25', '3002345678', 'Carrera 80 # 12-34, Itagüí, Antioquia', 2),
('1003456789', 'Luis Enrique Martínez', '1982-11-30', '3003456789', 'Calle 10 # 67-89, Rionegro, Antioquia', 3),
('1004567890', 'María Fernanda López', '1995-04-14', '3004567890', 'Avenida Las Vegas # 32-10, Envigado, Antioquia', 4),
('1005678901', 'José Andrés Pérez', '1983-06-05', '3005678901', 'Carrera 50 # 21-50, Medellín, Antioquia', 5),
('1006789012', 'Carolina Jiménez', '1992-02-17', '3006789012', 'Calle 80 # 14-21, Bello, Antioquia', 6),
('1007890123', 'Pedro Alejandro Rodríguez', '1988-09-03', '3007890123', 'Carrera 70 # 8-90, La Ceja, Antioquia', 1),
('1008901234', 'Verónica Gómez Ríos', '1993-12-22', '3008901234', 'Calle 15 # 23-45, Sabaneta, Antioquia', 2),
('1009012345', 'Ricardo Sánchez Martínez', '1987-05-30', '3009012345', 'Carrera 40 # 15-56, Itagüí, Antioquia', 3),
('1010123456', 'Laura Isabel García', '1997-01-14', '3000123456', 'Calle 12 # 50-60, Apartadó, Antioquia', 4);
go
INSERT INTO Sede (Nombre, Direccion, Telefono, CodigoCiudad) VALUES 
('Sede Centro', 'Calle 50 # 10-20', '3001234567', 1),
('Sede El Poblado', 'Carrera 43A # 5-15', '3002345678', 1),
('Sede Laureles', 'Avenida 80 # 30-40', '3003456789', 1),
('Sede Envigado', 'Calle 4 Sur # 35-60', '3004567890', 1),
('Sede Itagüí', 'Carrera 51 # 22-10', '3005678901', 1),
('Sede Bello', 'Calle 10 # 50-15', '3006789012', 1);
go
INSERT INTO EmpleadoCargo (FechaInicio, FechaFin, CodigoCargo, CCEmpleado, IdSede) VALUES
('2023-01-10 08:00:00', '2024-01-10 17:00:00', 1, '1001234567', 2),
('2023-03-05 08:00:00', '2023-12-05 17:00:00', 2, '1002345678', 2),
('2022-06-15 08:00:00', '2023-06-15 17:00:00', 3, '1003456789', 3),
('2023-05-01 08:00:00', NULL, 4, '1004567890', 4),
('2023-07-20 08:00:00', '2024-07-20 17:00:00', 5, '1005678901', 5),
('2023-02-25 08:00:00', '2024-02-25 17:00:00', 6, '1006789012', 6);
go
INSERT INTO Factura (Numero, Documento, Fecha, CodigoEmpleCargo) VALUES
(1001, '1002456789', '2023-05-15 10:00:00', 1),
(1002, '1009876543', '2023-06-01 14:30:00', 2),
(1003, '1023456789', '2023-07-20 09:45:00', 3),
(1004, '1034567890', '2023-08-10 11:00:00', 4),
(1005, '1045678901', '2023-09-05 16:15:00', 5),
(1006, '1056789012', '2023-10-01 13:00:00', 6);
go
INSERT INTO Servicio (Nombre, Descripcion, Precio) VALUES
('Reparación de Electrodomésticos', 'Reparación y mantenimiento de electrodomésticos como neveras, lavadoras, microondas, entre otros.', 150000),
('Reparación de Fontanería', 'Servicio de reparación y mantenimiento de tuberías, grifos, inodoros y sistemas de fontanería en general.', 120000),
('Reparación de Techo', 'Reparación de techos dañados o goteras en viviendas o locales comerciales.', 200000),
('Pintura de Interiores', 'Servicio de pintura de interiores de casas, oficinas y edificios, incluyendo preparación de superficies.', 180000),
('Instalación de Aire Acondicionado', 'Instalación de equipos de aire acondicionado, mantenimiento y reparación de los mismos.', 250000),
('Electricidad Básica', 'Reparación de sistemas eléctricos, cambio de enchufes, interruptores y reparación de cortocircuitos.', 140000),
('Reparación de Muebles', 'Restauración y reparación de muebles dañados, incluyendo cambios de tapicería, madera o estructura.', 100000),
('Instalación de Pisos', 'Instalación de pisos de cerámica, madera, laminado, entre otros tipos de materiales.', 220000),
('Desatascos de Alcantarillado', 'Desatasco de alcantarillado, reparaciones y limpieza de drenajes y tuberías bloqueadas.', 130000),
('Reparación de Ventanas', 'Servicio de reparación y mantenimiento de ventanas de todo tipo, cambio de vidrios y marcos.', 110000);
go
INSERT INTO DetalleFactura (Numero, CodigoServicio, Cantidad, Subtotal) VALUES
(1001, 1, 1, 150000), 
(1001, 2, 2, 240000), 
(1002, 3, 1, 200000), 
(1002, 4, 1, 180000),
(1003, 5, 1, 250000),  
(1003, 6, 3, 420000),  
(1004, 7, 1, 100000),  
(1005, 8, 2, 440000),  
(1005, 9, 1, 130000), 
(1006, 10, 1, 110000);
go
INSERT INTO Perfil (Nombre, PaginaNavegar) VALUES ('Administrador', 'Inicio.html'), ('Empleado', 'Empleado.html')
go
INSERT INTO Equipo (CodigoEquipo, Nombre, Descripcion, Cantidad) VALUES
(1, 'Taladro Eléctrico', 'Taladro profesional para perforación de paredes y superficies duras, utilizado en instalaciones eléctricas y de fontanería.', 5),
(2, 'Soplete de Oxígeno', 'Soplete para soldadura y corte de metal, utilizado en reparaciones de estructuras metálicas y fontanería industrial.', 2),
(3, 'Cortadora de Cerámica', 'Cortadora manual para cerámica y azulejos, utilizada en la instalación de pisos y remodelaciones de baños.', 4),
(4, 'Compresor de Aire', 'Compresor portátil para trabajos de pintura, limpieza y otras tareas que requieren aire comprimido.', 3),
(5, 'Sierra de Calar', 'Herramienta eléctrica para cortar madera, PVC y otros materiales, utilizada en trabajos de carpintería y remodelaciones.', 6),
(6, 'Extractor de Ventilación', 'Ventiladores y extractores de aire para la ventilación de espacios y extracción de aire caliente o húmedo.', 7),
(7, 'Soldadora', 'Máquina para soldar metales, utilizada en reparaciones de estructuras metálicas y trabajos de carpintería metálica.', 2),
(8, 'Medidor Láser', 'Medidor de distancia láser para obtener medidas precisas en instalaciones y remodelaciones.', 4),
(9, 'Escalera Telescópica', 'Escalera extensible para alcanzar lugares altos en reparaciones de techos, pintura y mantenimiento de instalaciones.', 5),
(10, 'Lijadora Eléctrica', 'Lijadora para pulir superficies de madera y metal, utilizada en trabajos de carpintería y restauración de muebles.', 3);
go
INSERT INTO Proveedor (IdProveedor, Nombre, RazonSocial, Direccion, Telefono, SitioWeb) VALUES
('PROV001', 'Equipos & Herramientas S.A.S.', 'Suministro de equipos, herramientas y materiales para reparación y remodelación en el hogar.', 'Calle 45 # 22-35, Medellín, Antioquia', '3001234567', 'www.equiposherramientas.com');
go
INSERT INTO Telefono (Numero, Documento) VALUES
('3001122334', '1002456789'),
('3002233445', '1009876543'),
('3003344556', '1023456789'),
('3004455667', '1034567890'),
('3005566778', '1045678901'),
('3006677889', '1056789012');
go
INSERT INTO Usuario (CCEmpleado, UserName, Clave, Salt) VALUES
('1001234567', 'carlos', 'cxd2KSWWnAaJYBn6XBQoO5Xwc0ne//CUbtyqZqNGgT91GREBY/tLK7/H4rJaG7TX', 'cxd2KSWWnAaJYBn6XBQoOw=='),
('1002345678', 'ana.g', 'JZO/9AdmCvMOpq2X0V+2AvhoZFcUNQNxh+jEbr6IGjvigYgcPm1j+7UIAXKbeOnd', 'JZO/9AdmCvMOpq2X0V+2Ag=='),
('1003456789', 'luis', 'tOy3hHP35Ml24+5ZVvVvh4qdcUnH0cAn41+Baomfa0L89nFAUZPEaO97HC+9bfPn', 'tOy3hHP35Ml24+5ZVvVvhw=='),
('1004567890', 'maria.lopez', 'MJSbp6o4EAP3U/39QIWrt85qUE/hA/wHZVB0SQgptVbePPfdaI8kRXa0Wcy1Ouuk', '9gSe/fxSBpdNApnu+w6liA=='),
('1005678901', 'jose', 'wVNSdzowC2k/447qOqYWNNK5+ICXtAbO8V822t/aNHo4UhLu0ipV6cOUZLBityP6', 'wVNSdzowC2k/447qOqYWNA=='),
('1006789012', 'carolina.jimenez', 'IM2PohKacv9Q8d2Fq7r+o7CRQvhsvsWwYI9N1oAXxt89wYWlKFfKie8iDPuvQxtf', 'IM2PohKacv9Q8d2Fq7r+ow==');
go
INSERT INTO PerfilUsuario (IdUsuario, IdPerfil, Estado) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 1, 1),
(4, 1, 1),
(5, 2, 0),
(6, 2, 1);
go
INSERT INTO Programacion (Fecha, Hora, Direccion, Estado, Documento, CodigoServicio) VALUES
('2025-05-23', '10:30', 'Calle 45 # 23-11, Medellín, Antioquia', 'agendada', '1002456789', 1),
('2025-05-24', '14:00', 'Carrera 80 # 12-34, Itagüí, Antioquia', 'asignada', '1009876543', 2),
('2025-05-25', '09:00', 'Avenida Las Vegas # 32-10, Envigado, Antioquia', 'en camino', '1023456789', 3),
('2025-05-26', '11:30', 'Calle 10 # 67-89, Rionegro, Antioquia', 'cumplida', '1034567890', 4),
('2025-05-27', '16:00', 'Carrera 50 # 21-50, Medellín, Antioquia', 'registrada', '1045678901', 5),
('2025-05-28', '13:15', 'Calle 80 # 14-21, Bello, Antioquia', 'agendada', '1056789012', 6);
