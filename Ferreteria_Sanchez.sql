/*
	Hoja de desarrollo - Base de Datos Sanchez
*/

use master;
go

/*
	Script para obtener el string de conexion de tu PC
*/

select
    'data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN'
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name() + ';password=<<YourPassword>>'
    end
    as ConnectionString
from sys.server_principals
where name = suser_name()

/*

*/

drop database ferreteria_sanchez;

create database ferreteria_sanchez;
use ferreteria_sanchez;

create table usuarios(
	id_usuario int identity(1,1) primary key,
	nombre_usuario varchar(50),
	contrasena varchar(100),
	tipo_user int, 
	/*
		1 = Administrador
		2 = Usuario Comun
	*/
	foto_user varchar(150)
);

create table productos(
	id_productos int identity(1,1) primary key,
	nombre_producto varchar(100),
	marca_producto varchar(100),
	num_stock int not null,
	precio_unitario float not null,
	categoria varchar(150),
	tags varchar(500),
	foto_producto varchar(150)
);

select * from usuarios;

select * from productos;

insert into usuarios(nombre_usuario, contrasena, tipo_user)values('dsalguera','Windowsxp',1);

select contrasena from usuarios where nombre_usuario = 'dsalguera';

select * from productos where id_productos=4

/*
	Fin de la Hoja
*/