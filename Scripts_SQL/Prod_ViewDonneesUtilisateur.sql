CREATE VIEW DonneesUtilisateur AS
	SELECT d.valLumiere, d.valSon, d.dateHeure, u.nomUtilisateur, u.prenomUtilisateur, u.age 
	FROM Donnees d 
	JOIN Utilisateurs u 
	ON d.noUtilisateur = u.noUtilisateur;
GO