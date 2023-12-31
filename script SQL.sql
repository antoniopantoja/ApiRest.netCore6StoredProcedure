USE [DesafioNET]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Logotipo] [varchar](300) NULL,
	[Dtcadastro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Endereco](
	[LogradouroId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClienteId] [bigint] NOT NULL,
	[Cep] [varchar](8) NULL,
	[Endereo] [varchar](100) NULL,
	[Bairro] [varchar](100) NULL,
	[Numero] [varchar](5) NULL,
	[Complemento] [varchar](100) NULL,
	[UF] [varchar](30) NULL,
	[Cidade] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogradouroId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log_Requisicao]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_Requisicao](
	[log_cdid] [bigint] IDENTITY(1,1) NOT NULL,
	[log_dtentrada] [datetime] NOT NULL,
	[log_Parametros] [varchar](max) NULL,
	[log_status] [varchar](60) NULL,
	[log_txtransacao] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_clientes]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****** Script do comando SelectTopNRows de SSMS  ******/

CREATE VIEW [dbo].[v_clientes]
AS
SELECT [ClienteId]
      ,[Nome]
      ,[Email]
      ,[Logotipo]
      ,[Dtcadastro]
  FROM [DesafioNET].[dbo].[Cliente]
GO
/****** Object:  View [dbo].[v_Enderecos]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[v_Enderecos]
AS
SELECT [LogradouroId]
      ,[ClienteId]
      ,[Cep]
      ,[Endereo]
      ,[Bairro]
      ,[Numero]
      ,[Complemento]
      ,[UF]
      ,[Cidade]
  FROM [DesafioNET].[dbo].[Endereco]
GO
ALTER TABLE [dbo].[Cliente] ADD  CONSTRAINT [Dtcadastro]  DEFAULT (getdate()) FOR [Dtcadastro]
GO
ALTER TABLE [dbo].[Endereco]  WITH CHECK ADD FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
/****** Object:  StoredProcedure [dbo].[usp_p_cliente]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC [dbo].[usp_p_cliente] 'I',1,'Pantoja de Souza','teste02@gmail.com','http://201.33.18.7:91/Imagens/foto%20Perfil-3.jpeg'
--EXEC [dbo].[usp_p_cliente] 'L',0,'',''
CREATE PROCEDURE [dbo].[usp_p_cliente] (
	@Operacao		    VARCHAR = 'I',
    @ClienteId          BIGINT = 0,
    @Nome               VARCHAR(100) = NULL,
    @Email				VARCHAR(100) = NULL,
	@Logotipo			VARCHAR(300) = NULL,
	@Code               INT = 0 OUTPUT,
	@Message            VARCHAR(999) = '' OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;
	---------
	-- LOG --
	---------
	-- #############################################################################
	DECLARE @log_Parametros VARCHAR(max) = NULL
	SET @log_Parametros = CONCAT(
	    '[dbo].[usp_p_cliente] ',
		'  @Operacao=', @Operacao,
		', @Idcliente=', ISNULL(CONVERT(VARCHAR, @ClienteId), 0),
		', @Nome=', ISNULL(@Nome, ''),
		', @Email=', ISNULL(@Email, ''),
		', @Logotipo=', ISNULL(@Logotipo, '')
	)	
	INSERT INTO [dbo].[Log_Requisicao]([log_dtentrada],[log_Parametros])
	VALUES (GETDATE(),@log_Parametros)
	-- #############################################################################

	-------------
	--VARIÁVEIS--
	-------------	
	DECLARE @retonologId INT = SCOPE_IDENTITY()
	DECLARE @ClieIDExiste INT = 0
	DECLARE @EmailExiste INT = 0
	DECLARE @retonoCliId INT = 0

	SET @ClieIDExiste = ISNULL((SELECT COUNT(*) 
										 FROM [dbo].[v_clientes] u
										 WHERE u.ClienteId = @ClienteId ), 0)

	SET @EmailExiste = ISNULL((SELECT COUNT(*) 
										FROM [dbo].[v_clientes] u
										WHERE u.Email = @Email
										AND (SELECT COUNT(*) 
										FROM [dbo].[v_clientes] u
										WHERE u.Email = @Email
										AND u.ClienteId = @ClienteId) <> 1 ), 0)

	IF (@EmailExiste > 0 AND @Operacao IN('I','U'))
		BEGIN
			SET @Code = 1
			SET @Message = 'Outro USUARIO no Sistema já possui o mesmo EMAIL informado!'
			UPDATE [dbo].[Log_Requisicao]
			SET log_status = 'ERRO INSERT',
				log_txtransacao = 'Outro USUARIO no Sistema já possui o mesmo EMAIL informado!'
			WHERE log_cdid = @retonologId
		END

	IF (@ClieIDExiste = 0 AND @Operacao IN('U','D'))
		BEGIN
		PRINT 'USUARIO não encontrado no Sistema'
			SET @Code = 1
			SET @Message = 'USUARIO não encontrado no Sistema!'
			UPDATE [dbo].[Log_Requisicao]
			SET log_status = 'ERRO USUARIO NÃO ENCONTRADO',
				log_txtransacao = 'USUARIO não encontrado no Sistema!'
			WHERE log_cdid = @retonologId
		END

	IF (@ClieIDExiste > 0 AND @Operacao = 'I')
		BEGIN
		PRINT 'OPERAÇÃO não corresponde com a solicitação'
			SET @Code = 1
			SET @Message = 'OPERAÇÃO não corresponde com a solicitação'
			UPDATE [dbo].[Log_Requisicao]
			SET log_status = 'ERRO Na OPERAÇÃO',
				log_txtransacao = 'ERRO Na OPERAÇÃO'
			WHERE log_cdid = @retonologId
		END
	------------
	--INSERÇÃO--
	------------
    IF (@Operacao = 'I' AND @EmailExiste = 0 AND @Code = 0)            
		BEGIN
			--
			INSERT INTO [dbo].[Cliente]
           ([Nome]
           ,[Email]
           ,[Logotipo]
           ,[Dtcadastro])
     VALUES
           (@Nome
           ,@Email
           ,@Logotipo
           ,GETDATE())

            --
			SET @retonoCliId = SCOPE_IDENTITY()
			SET @Code = @@ERROR
			--
			IF(@Code = 0)
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'SUCESSO INSERT',
					log_txtransacao = 'CADASTRO realizado com SUCESSO'
				WHERE log_cdid = @retonologId
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'ERRO INSERT',
					log_txtransacao = 'Não foi possível cadastrar. Por favor, tente novamente.'
				WHERE log_cdid = @retonologId
			END
			SET @Message = IIF(@Code = 0,'CADASTRO realizado com SUCESSO!<br/>','Não foi possível cadastrar. Por favor, tente novamente.<br/>')
		END

	-----------
	--DELEÇÃO--
	-----------
	IF (@Operacao = 'D' AND @ClieIDExiste > 0 AND @Code = 0)
		BEGIN
			--
			DELETE 
			FROM [dbo].[Endereco]
			WHERE ClienteId = @ClienteId
			--
			DELETE 
			FROM [dbo].[Cliente]
			WHERE ClienteId = @ClienteId
			--
			SET @Code = @@ERROR
			SET @Message = IIF(@Code = 0,'EXCLUSÃO realizada com SUCESSO!<br/>','ERRO no procedimento de EXCLUSÃO!<br/>')
			--
			IF(@Code = 0)
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'SUCESSO DELETE',
					log_txtransacao = 'EXCLUSÃO realizada com SUCESSO!'
				WHERE log_cdid = @retonologId
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'ERRO DELETE',
					log_txtransacao = 'ERRO no procedimento de EXCLUSÃO!'
				WHERE log_cdid = @retonologId
			END		
		END	
	-------------
	--ALTERAÇÃO--
	-------------
	IF (@Operacao = 'U' AND @ClieIDExiste > 0 AND @EmailExiste = 0 AND @Code = 0)
		BEGIN			

			DECLARE @query Nvarchar(MAX) = N'UPDATE [dbo].[Cliente]
											SET [Nome] ='''+@Nome+
											''',[Email] ='''+@Email+
											''''+IIF(@Logotipo <> '',',[Logotipo] ='''+@Logotipo+'''','')+CONCAT(' WHERE [ClienteId] =',@ClienteId)

			EXEC(@query)
			--
			SET @Code = @@ERROR
			SET @Message = IIF(@Code = 0,'DADOS atualizados com SUCESSO!<br/>','Não foi possível atualizar os dados. Por favor, tente novamente.<br/>')
			--
			IF(@Code = 0)
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'SUCESSO UPDATE',
					log_txtransacao = 'DADOS atualizados com SUCESSO!'
				WHERE log_cdid = @retonologId
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'ERRO UPDATE',
					log_txtransacao = 'Não foi possível atualizar os dados. Por favor, tente novamente.'
				WHERE log_cdid = @retonologId
			END
		END        
	------------
	---LISTAR---
	------------
    IF (@Operacao = 'L')            
		BEGIN
			EXEC [dbo].[usp_v_cliente] 
			 @Operacao
			,@ClienteId
			,@Nome
			,@Email
			,@Code OUTPUT
			,@Message OUTPUT
		END
	ELSE
		BEGIN
	    
			SELECT
			@Code AS 'Code',
			@Message AS 'Message',
			@retonoCliId AS 'Output'
			--
		END

	RETURN
END


GO
/****** Object:  StoredProcedure [dbo].[usp_p_clienteEndereco]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_p_clienteEndereco] (
	@Operacao		VARCHAR = 'I',
	@ClienteId      BIGINT = 0,
    @LogradouroId   BIGINT = 0,
    @Cep            VARCHAR(100) = NULL,
    @Logradura		VARCHAR(100) = NULL,
	@Bairro			VARCHAR(300) = NULL,
	@Numero         VARCHAR(300) = NULL,
	@Complemento    VARCHAR(300) = NULL,
	@Uf             VARCHAR(300) = NULL,
	@Cidade         VARCHAR(300) = NULL,
	@Code           INT = 0 OUTPUT,
	@Message        VARCHAR(999) = '' OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;
	---------
	-- LOG --
	---------
	-- #############################################################################
	DECLARE @log_Parametros VARCHAR(max) = NULL
	SET @log_Parametros = CONCAT(
	    '[dbo].[usp_p_clienteEndereco] ',
		'  @Operacao=', @Operacao,
		', @ClienteId=', ISNULL(CONVERT(VARCHAR, @ClienteId), 0),
		', @LogradouroId=', ISNULL(CONVERT(VARCHAR, @LogradouroId), 0),
		', @Cep=', ISNULL(@Cep, ''),
		', @Logradura=', ISNULL(@Logradura, ''),
		', @Bairro=', ISNULL(@Bairro, ''),
		', @Numero=', ISNULL(@Numero, ''),
		', @Complemento=', ISNULL(@Complemento, ''),
		', @Uf=', ISNULL(@Uf, ''),
		', @Cidade=', ISNULL(@Cidade, '')
	)	
	INSERT INTO [dbo].[Log_Requisicao]([log_dtentrada],[log_Parametros])
	VALUES (GETDATE(),@log_Parametros)
	-- #############################################################################

	-------------
	--VARIÁVEIS--
	-------------	
	DECLARE @retonologId INT = SCOPE_IDENTITY()
	DECLARE @ClieIDExiste INT = 0
	DECLARE @EndIDExiste INT = 0

	SET @ClieIDExiste = ISNULL((SELECT COUNT(*) 
										 FROM [dbo].[v_clientes] u
										 WHERE u.ClienteId = @ClienteId ), 0)

	SET @EndIDExiste = ISNULL((SELECT COUNT(*) 
										FROM [dbo].[v_Enderecos] u
										WHERE u.LogradouroId = @LogradouroId), 0)

	IF (@ClieIDExiste = 0 AND @Operacao = 'I')
		BEGIN
			SET @Code = 1
			SET @Message = 'USUARIO não encontrado no Sistema'
			UPDATE [dbo].[Log_Requisicao]
			SET log_status = 'ERRO USUARIO NÃO ENCONTRADO',
				log_txtransacao = 'USUARIO não encontrado no Sistema!'
			WHERE log_cdid = @retonologId
		END

	IF (@EndIDExiste = 0 AND @Operacao IN('U','D'))
		BEGIN
		PRINT 'ENDREREÇO não encontrado no Sistema'
			SET @Code = 1
			SET @Message = 'ENDREREÇO não encontrado no Sistema!'
			UPDATE [dbo].[Log_Requisicao]
			SET log_status = 'ERRO ENDREREÇO NÃO ENCONTRADO',
				log_txtransacao = 'ENDREREÇO não encontrado no Sistema!'
			WHERE log_cdid = @retonologId
		END

	------------
	--INSERÇÃO--
	------------
    IF (@Operacao = 'I' AND @ClieIDExiste > 0)            
		BEGIN
			--
			INSERT INTO [dbo].[Endereco]
				   ([ClienteId]
				   ,[Cep]
				   ,[Endereo]
				   ,[Bairro]
				   ,[Numero]
				   ,[Complemento]
				   ,[UF]
				   ,[Cidade])
			 VALUES
				   (@ClienteId
				   ,@Cep
				   ,@Logradura
				   ,@Bairro
				   ,@Numero
				   ,@Complemento
				   ,@UF
				   ,@Cidade)

            --
			SET @Code = @@ERROR
			--
			IF(@Code = 0)
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'SUCESSO INSERT ENDEREÇO',
					log_txtransacao = 'CADASTRO realizado com SUCESSO'
				WHERE log_cdid = @retonologId
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'ERRO INSERT ENDEREÇO',
					log_txtransacao = 'Não foi possível cadastrar. Por favor, tente novamente.'
				WHERE log_cdid = @retonologId
			END
			SET @Message = IIF(@Code = 0,'CADASTRO realizado com SUCESSO!<br/>','Não foi possível cadastrar. Por favor, tente novamente.<br/>')
		END

	-----------
	--DELEÇÃO--
	-----------
	IF (@Operacao = 'D' AND @EndIDExiste > 0)
		BEGIN
			--
			DELETE 
			FROM [dbo].[Endereco]
			WHERE LogradouroId = @LogradouroId
			--
			SET @Code = @@ERROR
			SET @Message = IIF(@Code = 0,'EXCLUSÃO realizada com SUCESSO!<br/>','ERRO no procedimento de EXCLUSÃO!<br/>')
			--
			IF(@Code = 0)
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'SUCESSO DELETE ENDEREÇO',
					log_txtransacao = 'EXCLUSÃO realizada com SUCESSO!'
				WHERE log_cdid = @retonologId
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'ERRO DELETE ENDEREÇO',
					log_txtransacao = 'ERRO no procedimento de EXCLUSÃO!'
				WHERE log_cdid = @retonologId
			END		
		END	
	-------------
	--ALTERAÇÃO--
	-------------
	IF (@Operacao = 'U' AND @EndIDExiste > 0)
		BEGIN			

		  UPDATE [dbo].[Endereco]
			SET  [Cep] = @Cep
				,[Endereo] = @Logradura
				,[Bairro] = @Bairro
				,[Numero] = @Numero
				,[Complemento] = @Complemento
				,[UF] = @UF
				,[Cidade] = @Cidade
				 WHERE [LogradouroId] = @LogradouroId	
			--
			SET @Code = @@ERROR
			SET @Message = IIF(@Code = 0,'DADOS atualizados com SUCESSO!<br/>','Não foi possível atualizar os dados. Por favor, tente novamente.<br/>')
			--
			IF(@Code = 0)
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'SUCESSO UPDATE ENDEREÇO',
					log_txtransacao = 'DADOS atualizados com SUCESSO!'
				WHERE log_cdid = @retonologId
			END
			ELSE
			BEGIN
				UPDATE [dbo].[Log_Requisicao]
				SET log_status = 'ERRO UPDATE ENDEREÇO',
					log_txtransacao = 'Não foi possível atualizar os dados. Por favor, tente novamente.'
				WHERE log_cdid = @retonologId
			END
		END        
	--	    
    SELECT
	@Code AS 'Code',
	@Message AS 'Message',
	0 AS 'Output'
	--
	RETURN
END


GO
/****** Object:  StoredProcedure [dbo].[usp_v_cliente]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC [dbo].[usp_v_cliente] 'L',0,'',''

CREATE PROCEDURE [dbo].[usp_v_cliente] (
	@Operacao		    VARCHAR = 'L',
    @ClienteId          BIGINT = 0,
    @Nome               VARCHAR(100) = NULL,
    @Email				VARCHAR(100) = NULL,
	@Code               INT = 0 OUTPUT,
	@Message            VARCHAR(999) = '' OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;
	---------
	-- LOG --
	---------
	-- #############################################################################
	DECLARE @log_Parametros VARCHAR(max) = NULL
	SET @log_Parametros = CONCAT(
	    '[dbo].[usp_v_cliente] ',
		'  @Operacao=', @Operacao,
		', @Idcliente=', ISNULL(CONVERT(VARCHAR, @ClienteId), 0),
		', @Nome=', ISNULL(@Nome, ''),
		', @Email=', ISNULL(@Email, '')
	)	
	INSERT INTO [dbo].[Log_Requisicao]([log_dtentrada],[log_Parametros])
	VALUES (GETDATE(),@log_Parametros)
	-- #############################################################################
	
	SELECT  
	   [ClienteId]
      ,[Nome]
      ,[Email]
      ,[Logotipo]
      ,FORMAT ([Dtcadastro], 'dd/MM/yyyy') AS 'Dtcadastro'
	FROM [DesafioNET].[dbo].[v_clientes]
	WHERE (ClienteId = @ClienteId OR @ClienteId = 0)
	AND ([Nome] LIKE '%'+@Nome+'%' OR @Nome IN ('',NULL))
	AND ([Email] LIKE '%'+@Email+'%' OR @Email IN ('',NULL))
	--
	RETURN
END


GO
/****** Object:  StoredProcedure [dbo].[usp_v_clienteEndereco]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC [dbo].[usp_v_clienteEndereco] 'L',0

CREATE PROCEDURE [dbo].[usp_v_clienteEndereco] (
	@Operacao		    VARCHAR = 'L',
    @ClienteId          BIGINT = 0,
	@Code               INT = 0 OUTPUT,
	@Message            VARCHAR(999) = '' OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;
	---------
	-- LOG --
	---------
	-- #############################################################################
	DECLARE @log_Parametros VARCHAR(max) = NULL
	SET @log_Parametros = CONCAT(
	    '[dbo].[usp_v_clienteEndereco] ',
		'  @Operacao=', @Operacao,
		', @Idcliente=', ISNULL(CONVERT(VARCHAR, @ClienteId), 0)
	)	
	INSERT INTO [dbo].[Log_Requisicao]([log_dtentrada],[log_Parametros])
	VALUES (GETDATE(),@log_Parametros)
	-- #############################################################################

	SELECT 
		[LogradouroId]
		,[ClienteId]
		,[Cep]
		,[Endereo] AS 'Logradura'
		,[Bairro]
		,[Numero]
		,[Complemento]
		,[UF]
		,[Cidade]
	FROM [DesafioNET].[dbo].[Endereco]	
	WHERE (ClienteId = @ClienteId OR @ClienteId = 0)
	--
	RETURN
END


GO
/****** Object:  StoredProcedure [dbo].[usp_v_dashboard]    Script Date: 20/11/2023 02:45:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC [dbo].[usp_v_dashboard] 'L'

CREATE PROCEDURE [dbo].[usp_v_dashboard] (
	@Operacao		    VARCHAR = 'L'
)
AS 
BEGIN
	SET NOCOUNT ON;
	---------
	-- LOG --
	---------
	-- #############################################################################
	DECLARE @log_Parametros VARCHAR(max) = NULL
	SET @log_Parametros = CONCAT(
	    '[dbo].[usp_v_dashboard] ',
		'  @Operacao=', @Operacao
	)	
	INSERT INTO [dbo].[Log_Requisicao]([log_dtentrada],[log_Parametros])
	VALUES (GETDATE(),@log_Parametros)
	-- #############################################################################
	

	SELECT 
		(SELECT COUNT([log_cdid])FROM [DesafioNET].[dbo].[Log_Requisicao] WHERE [log_Parametros] LIKE '%usp_p_cliente%' AND log_status = 'SUCESSO INSERT') AS 'ClientesCadastrados',
		(SELECT COUNT([log_cdid]) FROM [DesafioNET].[dbo].[Log_Requisicao] WHERE [log_Parametros] LIKE '%usp_p_cliente%'AND log_status = 'SUCESSO UPDATE') AS 'RegistroAlterados',
		(SELECT COUNT([log_cdid]) FROM [DesafioNET].[dbo].[Log_Requisicao] WHERE [log_Parametros] LIKE '%usp_p_cliente%' AND log_status = 'SUCESSO DELETE') AS 'ClientesExcluIdos',
		(SELECT COUNT([log_cdid]) FROM [DesafioNET].[dbo].[Log_Requisicao]) AS 'LogsRequisicao'

	--
	RETURN
END


GO
