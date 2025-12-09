USE PROG3A25_Production_AllysonJad;
IF OBJECT_ID('dbo.InsertionDonneesUtilisateur', 'TR') IS NOT NULL
    DROP TRIGGER dbo.InsertionDonneesUtilisateur;
GO

CREATE TRIGGER InsertionDonneesUtilisateur
ON DonneesCalendrier
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @noUtilisateur INT;
    DECLARE @valLumiere FLOAT;
    DECLARE @valSon FLOAT;
    DECLARE @dateHeure DATETIME;
    DECLARE @ressenti INT;
    DECLARE @heureLever DATETIME;
    DECLARE @heureCoucher DATETIME;
    DECLARE @dates DATE;

    DECLARE cDonnees CURSOR FOR
    SELECT noUtilisateur, valLumiere, valSon, dateHeure, ressenti,
    heureCoucher, heureLever, dates
    FROM inserted;

    OPEN cDonnees;
    FETCH cDonnees INTO @noUtilisateur, @valLumiere, @valSon, @dateHeure,
    @ressenti, @heureCoucher, @heureLever, @dates;

    WHILE(@@FETCH_STATUS = 0)
    BEGIN
        IF(@valLumiere IS NOT NULL AND @valSon IS NOT NULL AND @dateHeure IS NOT NULL)
        BEGIN
            INSERT INTO Donnees(noUtilisateur, valLumiere, valSon, dateHeure)
            VALUES(@noUtilisateur, @valLumiere, @valSon, @dateHeure);
        END;

        IF(@ressenti IS NOT NULL AND @heureCoucher IS NOT NULL AND @heureLever IS NOT NULL AND @dates IS NOT NULL)
        BEGIN
            INSERT INTO Calendrier(noUtilisateur, ressenti, heureCoucher, heureLever, dates)
            VALUES(@noUtilisateur, @ressenti, @heureCoucher, @heureLever, @dates);
        END;

        FETCH cDonnees INTO @noUtilisateur, @valLumiere, @valSon, @dateHeure, @ressenti,
        @heureCoucher, @heureLever, @dates;
    END;

    CLOSE cDonnees;
    DEALLOCATE cDonnees;
    SET NOCOUNT OFF;
END;

---------------------------
-- Alterner le trigger (modifier la logique)
