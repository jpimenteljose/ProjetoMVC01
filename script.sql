﻿CREATE TABLE EMPRESA(
	IDEMPRESA		UNIQUEIDENTIFIER	NOT NULL,
	NOMEFANTASIA	NVARCHAR(150)		NOT NULL,
	RAZAOSOCIAL		NVARCHAR(100)		NOT NULL,
	CNPJ			NVARCHAR(25)		NOT NULL UNIQUE,
	PRIMARY KEY(IDEMPRESA))
GO

CREATE TABLE FUNCIONARIO(
	IDFUNCIONARIO	UNIQUEIDENTIFIER	NOT NULL,
	NOME			NVARCHAR(150)		NOT NULL,
	CPF				NVARCHAR(15)		NOT NULL UNIQUE,
	MATRICULA		NVARCHAR(20)		NOT NULL,
	DATAADMISSAO	DATE				NOT NULL,
	IDEMPRESA		UNIQUEIDENTIFIER	NOT NULL,
	PRIMARY KEY(IDFUNCIONARIO),
	FOREIGN KEY(IDEMPRESA) REFERENCES EMPRESA(IDEMPRESA))
GO