USE PROG3A25_Production_AllysonJad;
GO
CREATE VIEW calendrierUtilisateur AS
SELECT 
    heureCoucher, 
    heureLever, 
    dates, 
    ressenti, 
    nbreReveil,  
    nomUtilisateur, 
    prenomUtilisateur, 
    age 
FROM Calendrier AS c
JOIN Utilisateurs AS u 
    ON c.noUtilisateur = u.noUtilisateur;
GO

SELECT * FROM calendrierUtilisateur;


ALTER VIEW calendrierUtilisateur AS  -- modification car j'ai fait une erreur (il manquait noUtilisateur)
SELECT 
	u.noUtilisateur,
    heureCoucher, 
    heureLever, 
    dates, 
    ressenti, 
    nbreReveil,  
    nomUtilisateur, 
    prenomUtilisateur, 
    age 
FROM Calendrier AS c
JOIN Utilisateurs AS u 
    ON c.noUtilisateur = u.noUtilisateur;