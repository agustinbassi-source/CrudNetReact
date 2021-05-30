USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Insert
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptCreate]
  
          @Id INT OUTPUT,
          @ClientId INT = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

   INSERT INTO [dbo].[Receipt]
           (
          [ClientId]
           )
     VALUES
           (
         @ClientId
           )

 SELECT @Id = SCOPE_IDENTITY()
END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Update
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptUpdate]
  
          @Count INT OUTPUT,
          @Id INT,
          @ClientId INT = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

  UPDATE [dbo].[Receipt]
 SET 
          [ClientId] = @ClientId
 WHERE [Id] = @Id

 SELECT @Count = @@ROWCOUNT

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Delete
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDelete]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

DELETE FROM [dbo].[Receipt]
      WHERE Id = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Select
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptSelect]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

SELECT TOP (1) 
           [Id]
          ,[ClientId]
  FROM [CrudBassi].[dbo].[Receipt]
  WHERE [Id] = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: SelectList
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptSelectByPage]
  
  @TotalRows INT OUTPUT,
  @PageNumber INT,
  @PageSize INT
AS
BEGIN
 
 SET NOCOUNT ON;

 SELECT  @TotalRows =  COUNT(*) OVER() 
  FROM [CrudBassi].[dbo].[Receipt]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

SELECT
           [Id]
          ,[ClientId]
  FROM [CrudBassi].[dbo].[Receipt]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: SelectListGrid
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDetailSelectByReceiptId]
  
@ReceiptId int
AS
BEGIN
 
 SET NOCOUNT ON;

SELECT  
           [Id]
           ,[ReceiptId]
          ,[ProductId]
          ,[Amount]
  FROM [CrudBassi].[dbo].[ReceiptDetail]
  WHERE [ReceiptId] = @ReceiptId

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Insert
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDetailCreate]
  
          @Id INT OUTPUT,
          @ReceiptId INT,
          @ProductId INT = NULL,
          @Amount INT = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

   INSERT INTO [dbo].[ReceiptDetail]
           (
          [ReceiptId],
          [ProductId],
          [Amount]
           )
     VALUES
           (
         @ReceiptId,
         @ProductId,
         @Amount
           )

 SELECT @Id = SCOPE_IDENTITY()
END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Update
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDetailUpdate]
  
          @Count INT OUTPUT,
          @Id INT,
          @ProductId INT = NULL,
          @Amount INT = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

  UPDATE [dbo].[ReceiptDetail]
 SET 
          [ProductId] = @ProductId,
          [Amount] = @Amount
 WHERE [Id] = @Id

 SELECT @Count = @@ROWCOUNT

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Delete
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDetailDelete]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

DELETE FROM [dbo].[ReceiptDetail]
      WHERE Id = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Select
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDetailSelect]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

SELECT TOP (1) 
           [Id]
          ,[ProductId]
          ,[Amount]
  FROM [CrudBassi].[dbo].[ReceiptDetail]
  WHERE [Id] = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: SelectList
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDetailSelectByPage]
  
  @TotalRows INT OUTPUT,
  @PageNumber INT,
  @PageSize INT
AS
BEGIN
 
 SET NOCOUNT ON;

 SELECT  @TotalRows =  COUNT(*) OVER() 
  FROM [CrudBassi].[dbo].[ReceiptDetail]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

SELECT
           [Id]
          ,[ProductId]
          ,[Amount]
  FROM [CrudBassi].[dbo].[ReceiptDetail]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Insert
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procClientCreate]
  
          @Id INT OUTPUT,
          @Name VARCHAR(50) = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

   INSERT INTO [dbo].[Client]
           (
          [Name]
           )
     VALUES
           (
         @Name
           )

 SELECT @Id = SCOPE_IDENTITY()
END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Update
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procClientUpdate]
  
          @Count INT OUTPUT,
          @Id INT,
          @Name VARCHAR(50) = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

  UPDATE [dbo].[Client]
 SET 
          [Name] = @Name
 WHERE [Id] = @Id

 SELECT @Count = @@ROWCOUNT

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Delete
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procClientDelete]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

DELETE FROM [dbo].[Client]
      WHERE Id = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Select
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procClientSelect]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

SELECT TOP (1) 
           [Id]
          ,[Name]
  FROM [CrudBassi].[dbo].[Client]
  WHERE [Id] = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: SelectList
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procClientSelectByPage]
  
  @TotalRows INT OUTPUT,
  @PageNumber INT,
  @PageSize INT
AS
BEGIN
 
 SET NOCOUNT ON;

 SELECT  @TotalRows =  COUNT(*) OVER() 
  FROM [CrudBassi].[dbo].[Client]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

SELECT
           [Id]
          ,[Name]
  FROM [CrudBassi].[dbo].[Client]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Insert
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procProductCreate]
  
          @Id INT OUTPUT,
          @Name VARCHAR(50) = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

   INSERT INTO [dbo].[Product]
           (
          [Name]
           )
     VALUES
           (
         @Name
           )

 SELECT @Id = SCOPE_IDENTITY()
END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Update
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procProductUpdate]
  
          @Count INT OUTPUT,
          @Id INT,
          @Name VARCHAR(50) = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

  UPDATE [dbo].[Product]
 SET 
          [Name] = @Name
 WHERE [Id] = @Id

 SELECT @Count = @@ROWCOUNT

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Delete
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procProductDelete]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

DELETE FROM [dbo].[Product]
      WHERE Id = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Select
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procProductSelect]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

SELECT TOP (1) 
           [Id]
          ,[Name]
  FROM [CrudBassi].[dbo].[Product]
  WHERE [Id] = @Id

END
GO

USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: SelectList
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procProductSelectByPage]
  
  @TotalRows INT OUTPUT,
  @PageNumber INT,
  @PageSize INT
AS
BEGIN
 
 SET NOCOUNT ON;

 SELECT  @TotalRows =  COUNT(*) OVER() 
  FROM [CrudBassi].[dbo].[Product]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

SELECT
           [Id]
          ,[Name]
  FROM [CrudBassi].[dbo].[Product]
  ORDER BY [Id] desc 
  OFFSET @PageSize * (@PageNumber - 1) ROWS
  FETCH NEXT @PageSize ROWS ONLY;

END
GO

