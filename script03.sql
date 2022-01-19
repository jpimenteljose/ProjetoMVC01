﻿CREATE PROCEDURE SP_ALTERARPRODUTO
	@IDPRODUTO		UNIQUEIDENTIFIER,
	@NOME			NVARCHAR(150),
	@PRECO			DECIMAL(18,2),
	@QUANTIDADE		INTEGER
AS
BEGIN

	UPDATE PRODUTO SET
		NOME = @NOME,
		PRECO = @PRECO,
		QUANTIDADE = @QUANTIDADE
	WHERE
		IDPRODUTO = @IDPRODUTO

END
GO