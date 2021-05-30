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
CREATE OR ALTER PROCEDURE [dbo].[procProductCreate]
  
          @Id INT OUTPUT,
          @Name VARCHAR(50) = NULL
AS
BEGIN
 
 SET NOCOUNT ON;

   INSERT INTO [dbo].[Product]
           (
          [Name]
           )
     VALUES
           (
         @Name
           )

 SELECT @Id = SCOPE_IDENTITY()
END
GO
