--TRIGGER
USE PROG3A25_AllysonJad;
IF OBJECT_ID('dbo.InsertionDonneesUtilisateur', 'TR') IS NOT NULL
    DROP TRIGGER dbo.InsertionDonneesUtilisateur;
GO

CREATE TRIGGER InsertionDonneesUtilisateur
ON DonneesCalendrier
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE		@noUtilisateur	INT
	DECLARE		@valTemp		FLOAT
	DECLARE		@valSon			FLOAT
	DECLARE		@dateHeure		DATETIME
	DECLARE		@ressenti		INT
	DECLARE		@heureLever		DATETIME
	DECLARE		@heureCoucher	DATETIME
	DECLARE		@dates			DATE
	DECLARE		cDonnees		CURSOR FOR
	SELECT noUtilisateur, valTemperature, valSon, dateHeure, ressenti,
	heureCoucher, heureLever, dates
	FROM inserted;
	OPEN cDonnees
	FETCH cDonnees INTO @noUtilisateur, @valTemp, @valSon, @dateHeure,
	@ressenti, @heureCoucher, @heureLever, @dates;
	WHILE(@@FETCH_STATUS=0)
	BEGIN

		if(@valTemp IS NOT NULL AND @valSon IS NOT NULL AND @dateHeure IS NOT NULL)
		BEGIN
			INSERT INTO Donnees(noUtilisateur, valTemperature, valSon, dateHeure)
			VALUES(@noUtilisateur, @valTemp, @valSon, @dateHeure);
		END;

		if(@ressenti IS NOT NULL AND @heureCoucher IS NOT NULL AND @heureLever IS NOT NULL AND @dates IS NOT NULL)
		BEGIN
			INSERT INTO Calendrier(noUtilisateur,ressenti,heureCoucher,heureLever,dates)
			VALUES(@noUtilisateur, @ressenti, @heureCoucher, @heureLever, @dates);
		END;

		FETCH cDonnees INTO @noUtilisateur, @valTemp, @valSon, @dateHeure, @ressenti,
		@heureCoucher, @heureLever, @dates;

	END;
	CLOSE cDonnees;
	DEALLOCATE cDonnees;
	SET NOCOUNT OFF;
END;
GO
----------------------------------------------- 2nd Trigger---------------------------
--IF OBJECT_ID('dbo.Defaut_Medecin', 'TR') IS NOT NULL
--    DROP TRIGGER dbo.Defaut_Medecin;
--GO
--CREATE TRIGGER Defaut_Medecin
--ON Utilisateurs
--INSTEAD OF INSERT, UPDATE
--AS
--BEGIN
--	SET NOCOUNT ON;
--	DECLARE
--		@nom		NVARCHAR(100),
--        @prenom		NVARCHAR(100),
--        @courriel	NVARCHAR(255),
--        @motDePasse BINARY(64),
--        @medecin	BIT,
--        @medAttitre INT,
--        @age		INT,
--        @photo		VARBINARY(MAX),
--        @dateRdv	DATE,
--        @assurance	VARCHAR(9),
--        @config		BIT,
--        @sel		UNIQUEIDENTIFIER;

--    DECLARE cMed CURSOR FOR
--        SELECT nomUtilisateur, prenomUtilisateur, courriel, motDePasse, 
--               medecin, medecinAttitre, age, photo, dateRdv, 
--               assuranceSociale, config, sel
--        FROM inserted;
--	OPEN cMed;
--	FETCH cMed INTO @nom, @prenom, @courriel, @motDePasse, 
--        @medecin, @medAttitre, @age, @photo, @dateRdv, 
--        @assurance, @config, @sel;
--	WHILE(@@FETCH_STATUS=0)
--	BEGIN
--		IF((@medecin = 1 AND @medAttitre IS NULL) 
--			OR (@medecin = 0 AND @medAttitre IS NOT NULL))
--		BEGIN
--			PRINT 'Patient doit avoir un médecin attitré.';
--		END
--		BEGIN TRY
--			INSERT INTO Utilisateurs (
--                    nomUtilisateur, prenomUtilisateur, courriel, motDePasse, 
--                    medecin, medecinAttitre, age, photo, dateRdv, 
--                    assuranceSociale, config, sel)
--                VALUES (
--                    @nom, @prenom, @courriel, @motDePasse,
--                    @medecin, @medAttitre, @age, @photo, @dateRdv,
--                    @assurance, @config, @sel);
--		END TRY
--		BEGIN CATCH
--			PRINT('ERREUR INSERTION.')
--		END CATCH
--		FETCH cMed INTO 
--            @nom, @prenom, @courriel, @motDePasse, 
--            @medecin, @medAttitre, @age, @photo, @dateRdv, 
--            @assurance, @config, @sel;
--	END;
--	SET NOCOUNT OFF;
--	CLOSE cMed;
--	DEALLOCATE cMed;
--END;