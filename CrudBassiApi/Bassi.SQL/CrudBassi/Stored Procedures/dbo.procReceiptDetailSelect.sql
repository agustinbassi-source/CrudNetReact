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
