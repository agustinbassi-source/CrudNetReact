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
