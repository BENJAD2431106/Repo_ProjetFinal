CREATE VIEW MedecinUtilisateur AS
	SELECT u.noUtilisateur, u.nomUtilisateur, u.prenomUtilisateur, 
	u.age, u.photo, u.dateRdv, u.assuranceSociale, u.medecinAttitre,
	m.noUtilisateur AS noMedecin, m.prenomUtilisateur AS prenomMedecin, 
	m.nomUtilisateur AS nomMedecin
	FROM Utilisateurs u
	LEFT JOIN Utilisateurs m ON u.medecinAttitre=m.noUtilisateur
GO