CREATE VIEW [dbo].[DonneesCalendrier] AS
	SELECT u.noUtilisateur, ValTemperature, valSon, 
	dateHeure, c.ressenti, c.heureCoucher, c.heureLever, c.dates
	FROM Donnees d 
	JOIN Utilisateurs u ON d.noUtilisateur=u.noUtilisateur
	JOIN Calendrier c ON u.noUtilisateur=c.noUtilisateur;
GO


