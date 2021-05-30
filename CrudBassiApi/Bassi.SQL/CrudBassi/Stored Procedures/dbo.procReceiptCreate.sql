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
