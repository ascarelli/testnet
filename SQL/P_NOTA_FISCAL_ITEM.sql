USE [Teste]
GO
IF OBJECT_ID('dbo.P_NOTA_FISCAL_ITEM') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_NOTA_FISCAL_ITEM
    IF OBJECT_ID('dbo.P_NOTA_FISCAL_ITEM') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_NOTA_FISCAL_ITEM >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL_ITEM APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE P_NOTA_FISCAL_ITEM
(
	@pId int,
    @pIdNotaFiscal int,
    @pCfop varchar(5),
    @pTipoIcms varchar(20),
    @pBaseIcms decimal(18,5),
    @pAliquotaIcms decimal(18,5),
    @pValorIcms decimal(18,5),
    @pBaseIPI decimal(18,5),
    @pAliquotaIPI decimal(18,5),
    @pValorIPI decimal(18,5),
    @pNomeProduto varchar(50),
    @pCodigoProduto varchar(20),
	@pDesconto decimal(18,5)
)
AS
BEGIN
	IF (@pId = 0)
	BEGIN 		
		INSERT INTO [dbo].[NotaFiscalItem]
           ([IdNotaFiscal]
           ,[Cfop]
           ,[TipoIcms]
           ,[BaseIcms]
           ,[AliquotaIcms]
           ,[ValorIcms]
           ,[BaseIPI]
           ,[AliquotaIPI]
           ,[ValorIPI]
           ,[NomeProduto]
           ,[CodigoProduto]
		   ,[Desconto])
		VALUES
           (@pIdNotaFiscal,
			@pCfop,
			@pTipoIcms,
			@pBaseIcms,
			@pAliquotaIcms,
			@pValorIcms,
			@pBaseIPI,
			@pAliquotaIPI,
			@pValorIPI,
			@pNomeProduto,
			@pCodigoProduto,
			@pDesconto)

		SET @pId = @@IDENTITY
		Select @pId as Id
	END
	ELSE
	BEGIN
		UPDATE [dbo].[NotaFiscalItem]
		SET [IdNotaFiscal] = @pIdNotaFiscal
			,[Cfop] = @pCfop
			,[TipoIcms] = @pTipoIcms
			,[BaseIcms] = @pBaseIcms
			,[AliquotaIcms] = @pAliquotaIcms
			,[ValorIcms] = @pValorIcms
			,[BaseIPI] = @pBaseIPI
			,[AliquotaIPI] = @pAliquotaIPI
			,[ValorIPI] = @pValorIPI
			,[NomeProduto] = @pNomeProduto
			,[CodigoProduto] = @pCodigoProduto
			,[Desconto] = @pDesconto
		 WHERE Id = @pId
	END	    
END
GO
GRANT EXECUTE ON dbo.P_NOTA_FISCAL_ITEM TO [public]
go
IF OBJECT_ID('dbo.P_NOTA_FISCAL_ITEM') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL_ITEM CRIADA >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_NOTA_FISCAL_ITEM >>>'
go