USE PROG3A25_Production_AllysonJad;
GO
CREATE TRIGGER	 update_calendrier ON calendrierUtilisateur

INSTEAD OF INSERT -- au lieu d'inserer
AS
BEGIN 
	SET NOCOUNT ON;

	DECLARE @noUtilissateur		INT
	DECLARE @nomUtilisateur		DATETIME
	DECLARE @prenomUtilisateur  DATETIME
	DECLARE @age				INT
	DECLARE @heureCoucher		DATETIME
	DECLARE @heureLever			DATETIME
	DECLARE @dates				DATE
	DECLARE @ressenti			INT
	DECLARE @nbreReveil			INT
	DECLARE cCalendrier			CURSOR FOR -- déclare mon cursor
		SELECT 
			noUtilisateur, 
			nomUtilisateur, 
			prenomUtilisateur,
			age, 
			heureCoucher, 
			heureLever, 
			dates, 
			ressenti, 
			nbreReveil 
		FROM inserted; -- insert dans la table insertion
	OPEN cCalendrier;
	FETCH cCalendrier INTO @noUtilissateur, @nomUtilisateur, @prenomUtilisateur, @age, -- sauter de ligne
						   @heureCoucher, @heureLever, @dates, @ressenti, @nbreReveil;

	WHILE(@@FETCH_STATUS=0) -- si le fetch = 0 alors j'exécute la suite
	BEGIN

		IF (@nomUtilisateur IS NOT NULL AND @prenomUtilisateur IS NOT NULL AND @age IS NOT NULL)  -- regarde si les variables ne sont pas null, puis exécute le Insert into
			BEGIN
				INSERT INTO Utilisateurs (nomUtilisateur, prenomUtilisateur, age)
				VALUES (@nomUtilisateur, @prenomUtilisateur, @age);
			END;
		IF (@heureCoucher IS NOT NULL AND @heureLever IS NOT NULL AND @dates IS NOT NULL AND @ressenti IS NOT NULL AND @nbreReveil IS NOT NULL)
			BEGIN
				INSERT INTO Calendrier (heureCoucher, heureLever, dates, ressenti, nbreReveil)
				VALUES (@heureCoucher, @heureLever, @dates, @ressenti, @nbreReveil);
			END;

		FETCH cCalendrier INTO @noUtilissateur, @nomUtilisateur, @prenomUtilisateur, @age, -- sauter de ligne
						   @heureCoucher, @heureLever, @dates, @ressenti, @nbreReveil;
	END;
	CLOSE cCalendrier;
	DEALLOCATE cCalendrier;
END;