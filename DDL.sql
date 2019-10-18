/* Criar Banco */
CREATE DATABASE Gufos;

/* Usar Banco */
USE Gufos;

/* Criar uma tabela */
CREATE TABLE tipoUsuario(
IDTipoUsuario INT IDENTITY PRIMARY KEY, 
Titulo VARCHAR(255)  UNIQUE NOT NULL,
);

/* criar uma tabela de Usuario */
CREATE TABLE usuario(
IDUsuario INT IDENTITY PRIMARY KEY, 
Nome  VARCHAR(255)  NOT NULL,
Email VARCHAR(255)  UNIQUE NOT NULL,
Senha VARCHAR(255)  NOT NULL,

/*--  Chamamos nossa chaves estrabgeiras */

IDTipoUsuario INT FOREIGN KEY REFERENCES tipoUsuario (IDTipoUsuario),
);

/* criar uma tabela de Categoria */
CREATE TABLE categoria(
IDCategoria INT IDENTITY PRIMARY KEY, 
Titulo VARCHAR(255)  UNIQUE NOT NULL,
);

/* criar uma tabela de Localização */
CREATE TABLE localizacao(
IDLocalizacao INT IDENTITY PRIMARY KEY, 
CNPJ         CHAR(14)      NOT NULL,
RazãoSocial  VARCHAR(255)  UNIQUE NOT NULL,
Endereco     VARCHAR(255)  UNIQUE NOT NULL,
);

/* criar uma tabela de Eventos*/
/* BIT DEFAULT(1) é o boleano, ele será*/
CREATE TABLE evento(
IDEvento INT IDENTITY PRIMARY KEY NOT NULL,
Titulo VARCHAR(255) NOT NULL,
DataDoEvento DATETIME NOT NULL,
AcessoLivre BIT DEFAULT(1)  UNIQUE NOT NULL, 

/*--  Chamamos nossa chaves estrabgeiras */

IDCategoria INT FOREIGN KEY REFERENCES categoria      (IDCategoria),
IDLocalizacao INT FOREIGN KEY REFERENCES localizacao  (IDLocalizacao) 
);

/* criar uma tabela de presença */
CREATE TABLE presenca(
IDPresenca INT IDENTITY PRIMARY KEY, 
PresencaStatus VARCHAR(255)  UNIQUE NOT NULL,

/*--  Chamamos nossa chaves estrabgeiras */

IDUsuario INT FOREIGN KEY REFERENCES usuario      (IDUsuario),
IDEvento  INT FOREIGN KEY REFERENCES evento       (IDEvento) 
);