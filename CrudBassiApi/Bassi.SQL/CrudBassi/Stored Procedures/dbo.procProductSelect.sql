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
