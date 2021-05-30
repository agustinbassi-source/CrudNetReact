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
CREATE OR ALTER PROCEDURE [dbo].[procClientUpdate]
  
          @Count INT OUTPUT,
          @Id INT,
          @Name VARCHAR(50) = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

  UPDATE [dbo].[Client]
 SET 
          [Name] = @Name
 WHERE [Id] = @Id

 SELECT @Count = @@ROWCOUNT

END
GO
