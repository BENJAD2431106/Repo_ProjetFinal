DROP DATABASE IF EXISTS PROG3A25_Production_AllysonJad;
CREATE DATABASE PROG3A25_Production_AllysonJad;
USE PROG3A25_Production_AllysonJad;

CREATE TABLE Utilisateurs(
noUtilisateur			INT IDENTITY(1,1)		PRIMARY KEY,
nomUtilisateur			NVARCHAR(100)			NOT NULL,
prenomUtilisateur		NVARCHAR(100)			NOT NULL,
courriel				NVARCHAR(255)			NOT NULL,
motDePasse				BINARY(64)				NOT NULL,
medecin					BIT						NOT NULL,
medecinAttitre			INT						NULL,
age						INT						NOT NULL,
photo					VARBINARY(MAX)			NULL,
dateRdv					DATE					NULL,
assuranceSociale		VARCHAR(9)				NOT NULL,
config					BIT 					NULL,
sel						UNIQUEIDENTIFIER		NOT NULL
);

CREATE TABLE Donnees(
noDonnees				INT IDENTITY(1,1)		PRIMARY KEY,
valTemperature			FLOAT					NOT NULL,
valSon					FLOAT					NOT NULL,
dateHeure				DATETIME				NOT NULL,
noUtilisateur			INT						NOT NULL,
);


CREATE TABLE Calendrier(
noCalendrier			INT IDENTITY(1,1)		PRIMARY KEY,
heureCoucher			DATETIME				NOT NULL,
heureLever				DATETIME				NOT NULL,
dates					DATE					NOT NULL,
noUtilisateur			INT						NOT NULL,
ressenti				INT						NOT NULL,
nbreReveil				INT						NULL,
commentaire				NVARCHAR(255)			NULL,
);
------------
-- Modifier la colonne dans la table Donnees
USE PROG3A25_Production_AllysonJad
EXEC sp_rename 'Donnees.valTemperature', 'valLumiere', 'COLUMN';
--------------------------------------------------------------
-- Supprimer les vues existantes
USE PROG3A25_Production_AllysonJad
DROP VIEW IF EXISTS DonneesUtilisateur;
DROP VIEW IF EXISTS DonneesCalendrier;

-- Recréer les vues avec la colonne modifiée
CREATE VIEW DonneesUtilisateur AS
	SELECT valLumiere, valSon, dateHeure, nomUtilisateur, prenomUtilisateur, age 
	FROM Donnees d 
	JOIN Utilisateurs u 
	ON d.noUtilisateur = u.noUtilisateur;
GO

CREATE VIEW DonneesCalendrier AS
	SELECT u.noUtilisateur, valLumiere, valSon, 
	dateHeure, c.ressenti, c.heureCoucher, c.heureLever, c.dates
	FROM Donnees d 
	JOIN Utilisateurs u ON d.noUtilisateur = u.noUtilisateur
	JOIN Calendrier c ON u.noUtilisateur = c.noUtilisateur;
GO


