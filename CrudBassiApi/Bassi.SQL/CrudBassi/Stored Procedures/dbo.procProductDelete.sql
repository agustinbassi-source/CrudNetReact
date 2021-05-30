USE [CrudBassi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Generador
-- Create date: 30/5/2021
-- Description: Delete
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[procProductDelete]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

DELETE FROM [dbo].[Product]
      WHERE Id = @Id

END
GO
