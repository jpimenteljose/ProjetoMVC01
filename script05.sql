CREATE PROCEDURE SP_CONSULTARPRODUTOS
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
	ORDER BY NOME ASC

END
GO