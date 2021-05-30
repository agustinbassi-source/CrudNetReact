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
