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
