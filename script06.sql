﻿CREATE PROCEDURE SP_OBTERPRODUTOPORID
	@IDPRODUTO		UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
		IDPRODUTO,
		NOME,
		PRECO,
		QUANTIDADE,
		DATACADASTRO
	FROM 
	PRODUTO
	WHERE 
		IDPRODUTO = @IDPRODUTO

END
GO