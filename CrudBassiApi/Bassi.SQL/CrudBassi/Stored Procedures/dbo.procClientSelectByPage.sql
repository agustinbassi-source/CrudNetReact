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
