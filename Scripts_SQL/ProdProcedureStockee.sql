USE PROG3A25_Production_AllysonJad;
GO
CREATE PROCEDURE UP_InscrireUtilisateur
	@courriel		NVARCHAR(255),
	@nom			NVARCHAR(100), 
	@prenom			NVARCHAR(100), 
	@motDePasse		VARCHAR(255), 
	@photo			VARBINARY(MAX) = NULL, 
	@nas			VARCHAR(9), 
	@age			INT,
	@noUtilisateur	INT						OUTPUT
AS
BEGIN
	DECLARE			@no				INT;
	DECLARE			@sel			UNIQUEIDENTIFIER=NEWID(); 
	IF EXISTS (SELECT NoUtilisateur FROM Utilisateurs WHERE courriel=@courriel) 
	BEGIN
			SET @noUtilisateur=-1;
			RETURN -1
	END
	BEGIN TRY

		INSERT INTO Utilisateurs(courriel, nomUtilisateur, 
		prenomUtilisateur, motDePasse, assuranceSociale, age,
		photo, sel) VALUES
		(@courriel , @nom , @prenom, 
		HASHBYTES('SHA2_512',@motDePasse+CAST(@sel AS NVARCHAR(36))),
		@nas, @age, @photo, @sel);

			SELECT @no=noUtilisateur FROM Utilisateurs 
			WHERE courriel=@courriel;
			
		SET @noUtilisateur = @no;--scope_identity() 
	END TRY
	BEGIN CATCH
		SET @noUtilisateur = -2	
	END CATCH
END
--------------------------------------------------------------------------------------------------
USE PROG3A25_Production_AllysonJad
EXEC sp_rename 'Utilisateurs.assuranceSociale', 'ramQ', 'COLUMN';
ALTER TABLE Utilisateurs
ALTER COLUMN ramQ NVARCHAR(12) NOT NULL;
--------------------------------------------------------------------------------------------------
USE PROG3A25_Production_AllysonJad;
GO

ALTER PROCEDURE UP_InscrireUtilisateur
	@courriel		NVARCHAR(255),
	@nom			NVARCHAR(100), 
	@prenom			NVARCHAR(100), 
	@motDePasse		VARCHAR(255), 
	@photo			VARBINARY(MAX) = NULL, 
	@ramQ			VARCHAR(12), 
	@age			INT,
	@noUtilisateur	INT						OUTPUT
AS
BEGIN
	DECLARE @no			INT;
	DECLARE @sel		UNIQUEIDENTIFIER = NEWID(); 

	IF EXISTS (SELECT 1 FROM Utilisateurs WHERE courriel = @courriel)
	BEGIN
		SET @noUtilisateur = -1;
		RETURN -1;
	END

	BEGIN TRY
		INSERT INTO Utilisateurs
		(
			courriel, 
			nomUtilisateur, 
			prenomUtilisateur, 
			motDePasse, 
			ramQ, 
			age, 
			photo, 
			sel
		)
		VALUES
		(
			@courriel, 
			@nom, 
			@prenom,
			HASHBYTES('SHA2_512', @motDePasse + CAST(@sel AS NVARCHAR(36))),
			@ramQ, 
			@age, 
			@photo, 
			@sel
		);
		SET @noUtilisateur = SCOPE_IDENTITY();
	END TRY
	BEGIN CATCH
		SET @noUtilisateur = -2;
	END CATCH
END
GO
