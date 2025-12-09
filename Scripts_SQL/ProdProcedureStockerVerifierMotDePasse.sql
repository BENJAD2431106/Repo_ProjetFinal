USE PROG3A25_Production_AllysonJad;
GO

CREATE PROCEDURE UP_VerifierMotDePasse
    @courriel NVARCHAR(255),
    @motDePasseActuel NVARCHAR(255),
    @noUtilisateur INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Vérifie si l'utilisateur avec ce courriel et ce mot de passe existe
    IF NOT EXISTS(
        SELECT 1 
        FROM Utilisateurs
        WHERE courriel = @courriel
          AND motDePasse = HASHBYTES('SHA2_512', @motDePasseActuel + CAST(sel AS NVARCHAR(36)))
    )
    BEGIN
        -- Utilisateur introuvable ou mot de passe incorrect
        SET @noUtilisateur = -1;
        RETURN;
    END

    -- Si trouvé, renvoie le NoUtilisateur
    SET @noUtilisateur = (
        SELECT noUtilisateur
        FROM Utilisateurs
        WHERE courriel = @courriel
          AND motDePasse = HASHBYTES('SHA2_512', @motDePasseActuel + CAST(sel AS NVARCHAR(36)))
    );
END;
GO

--DECLARE @noUtilisateur INT;

--EXEC UP_VerifierMotDePasse
--    @courriel = 'ally@gmail.com',
--    @motDePasseActuel = '123456',
--    @noUtilisateur = @noUtilisateur OUTPUT;

--SELECT @noUtilisateur; -- 1 si trouvé, -1 sinon

