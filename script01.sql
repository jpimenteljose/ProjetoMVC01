﻿CREATE TABLE PRODUTO(
	IDPRODUTO		UNIQUEIDENTIFIER	NOT NULL,
	NOME			NVARCHAR(150)		NOT NULL,
	PRECO			DECIMAL(18,2)		NOT NULL,
	QUANTIDADE		INTEGER				NOT NULL,
	DATACADASTRO	DATETIME			NOT NULL,
	PRIMARY KEY(IDPRODUTO))
GO