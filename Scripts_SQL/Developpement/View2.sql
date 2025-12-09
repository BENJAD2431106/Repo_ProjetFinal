CREATE VIEW [dbo].[DonneesUtilisateur] AS
	SELECT valTemperature, valSon, dateHeure, nomUtilisateur, prenomUtilisateur, age 
	FROM Donnees d 
	JOIN Utilisateurs u 
	ON d.noUtilisateur=u.noUtilisateur;
GO


