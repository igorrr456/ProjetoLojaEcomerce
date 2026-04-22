-- criando o banco de dados
create database dbProjetoLoja;

-- usando o banco de dados 
use dbProjetoLoja;

-- criando tabela
create table tb_Usuario(
Id int primary key auto_increment,
Nome varchar(50) not null,
Email varchar(50) not null, 
Senha varchar(250) not null,
Nivel varchar (50) NOT null
);

-- Consultando o banco de dados 
Select * from tb_Usuario;

-- inserindo dados na tabela do banco de dados
Insert into tb_Usuario(Email,Senha,Nome,Nivel)Values('Admin@eamil.com','12345678','Administrador','Admin');