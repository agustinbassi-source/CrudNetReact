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
