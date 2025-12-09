USE PROG3A25_AllysonJad;
GO
CREATE PROCEDURE UP_ConnexionUtilisateur 
	@courriel NVARCHAR(255), 
	@motDePasse NVARCHAR(255),
	@noUtilisateur INT OUTPUT 
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS(SELECT * FROM Utilisateurs WHERE @courriel=courriel)
	BEGIN
		SET @noUtilisateur = -1; 
		RETURN;
	END 
	ELSE 
		SET @noUtilisateur =(SELECT noUtilisateur FROM Utilisateurs WHERE  @courriel=courriel AND motDePasse = HASHBYTES('SHA2_512', @motDePasse + CAST(sel AS NVARCHAR(36))));
END;
GO
DECLARE @noUtilisateur INT;
exec UP_ConnexionUtilisateur
	@courriel = 'allyson@gmail.com',
	@motDePasse = 'allyson',
	@noUtilisateur output
GO
SELECT @noUtilisateur
