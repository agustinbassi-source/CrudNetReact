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
