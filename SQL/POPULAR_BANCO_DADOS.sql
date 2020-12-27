USE [Teste]
GO
DECLARE @vIdNota int = 0
DECLARE @vNumeroNotaFiscal int = 1
DECLARE @vSerie int = 2
DECLARE @vNomeCliente varchar(50) = 'TESTE'
DECLARE @vEstadoDestino varchar(50) = 'SP'
DECLARE @vEstadoOrigem varchar(50) = 'RJ'
DECLARE @vCount INT = 0

DECLARE	@vIdItem int   
DECLARE @vCfop varchar(5)
DECLARE @vTipoIcms varchar(20)
DECLARE @vBaseIcms decimal(18,5)
DECLARE @vAliquotaIcms decimal(18,5)
DECLARE @vValorIcms decimal(18,5)
DECLARE @vBaseIPI decimal(18,5)
DECLARE @vAliquotaIPI decimal(18,5)
DECLARE @vValorIPI decimal(18,5)
DECLARE @vNomeProduto varchar(50)
DECLARE @vCodigoProduto varchar(20)
DECLARE @vDesconto decimal(18,5)

WHILE (@vCount <= 1000) 
BEGIN
	SET @vIdNota = 0
	SET @vNumeroNotaFiscal  = @vCount + 1
	SET @vSerie = 1
	SET @vNomeCliente = 'TESTE ' + CONVERT(VARCHAR, @vCount)

	IF (@vCount % 2) = 0
	BEGIN		
		SET @vEstadoOrigem = 'RJ'
		SET @vEstadoDestino = 'SP'
	END
	ELSE
	BEGIN		
		SET @vEstadoOrigem = 'MG'
		SET @vEstadoDestino = 'PE'
	END

	EXEC [dbo].[P_NOTA_FISCAL] 
		@pId = @vIdNota OUTPUT,
		@pNumeroNotaFiscal = @vNumeroNotaFiscal,
		@pSerie = @vSerie,
		@pNomeCliente = @vNomeCliente,
		@pEstadoDestino = @vEstadoDestino,
		@pEstadoOrigem = @vEstadoOrigem
		
	SET @vIdItem = 0   

	IF (@vCount % 2) = 0
		SET @vCfop = '6.102'
	ELSE
		SET @vCfop = '5.100'

		IF (select count(Id) from dbo.NotaFiscal nf where id = @vIdNota and nf.EstadoDestino = 'SP') = 1
		SET @vDesconto = '10'
	ELSE
		SET @vDesconto = '0'
	
	SET @vTipoIcms = '60'
	SET @vBaseIcms = 100.00
	SET @vAliquotaIcms = 10
	SET @vValorIcms = 10
	SET @vBaseIPI = 100.00
	SET @vAliquotaIPI = 10
	SET @vValorIPI = 10
	SET @vNomeProduto = 'PRODUTO DE CARGA'
	SET @vCodigoProduto = '123-5548-555-22'

	EXEC [dbo].[P_NOTA_FISCAL_ITEM]
		@pId = @vIdItem,
		@pIdNotaFiscal = @vIdNota,
		@pCfop = @vCfop,
		@pTipoIcms = @vTipoIcms,
		@pBaseIcms = @vBaseIcms,
		@pAliquotaIcms = @vAliquotaIcms,
		@pValorIcms = @vValorIcms,
		@pBaseIPI = @vBaseIPI,
		@pAliquotaIPI = @vAliquotaIPI,
		@pValorIPI = @vValorIPI,
		@pNomeProduto = @vNomeProduto,
		@pCodigoProduto = @vCodigoProduto,
		@pDesconto = @vDesconto

	SET @vCount = @vCount + 1
END

INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'RJ', 6.000);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'PE', 6.001);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'MG', 6.002);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'PB', 6.003);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'PR', 6.004);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'PI', 6.005);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'RO', 6.006);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'MT', 6.007);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'TO', 6.008);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'SE', 6.009);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('SP', 'PA', 6.010);

INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'RJ', 6.000);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'PE', 6.001);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'MG', 6.002);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'PB', 6.003);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'PR', 6.004);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'PI', 6.005);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'RO', 6.006);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'MT', 6.007);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'TO', 6.008);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'SE', 6.009);
INSERT INTO dbo.NotaFiscalCFOP (EstadoOrigem, EstadoDestino, ValorCFOP) values ('MG', 'PA', 6.010);