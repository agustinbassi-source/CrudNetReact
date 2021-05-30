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
