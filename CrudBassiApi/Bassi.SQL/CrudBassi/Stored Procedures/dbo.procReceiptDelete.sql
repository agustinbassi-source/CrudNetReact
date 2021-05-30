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
CREATE OR ALTER PROCEDURE [dbo].[procReceiptDelete]
  
@Id int
AS
BEGIN
 
 SET NOCOUNT ON;

DELETE FROM [dbo].[Receipt]
      WHERE Id = @Id

END
GO
