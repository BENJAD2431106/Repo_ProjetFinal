USE PROG3A25_AllysonJad;

ALTER TABLE Utilisateurs --cle etrangere medecin
ADD CONSTRAINT FK_Utilisateurs FOREIGN KEY (medecinAttitre)
        REFERENCES Utilisateurs(noUtilisateur);
ALTER TABLE Utilisateurs --par defaut pas de medecin assigne
ADD CONSTRAINT DF_Medecin DEFAULT 0 FOR medecin;
ALTER TABLE Utilisateurs --par defaut le site est clair (pas de données)
ADD CONSTRAINT DF_Config DEFAULT 1 FOR config;
ALTER TABLE Utilisateurs --email unique
ADD CONSTRAINT Unique_Courriel UNIQUE (courriel);
ALTER TABLE Utilisateurs --age depasse pas 120
ADD CONSTRAINT Check_Age CHECK (age BETWEEN 0 AND 120);
EXEC sp_rename 'Utilisateurs.assuranceSociale', 'ramQ', 'COLUMN';
ALTER TABLE Utilisateurs
ALTER COLUMN ramQ		char(12);
ALTER TABLE Donnees --cle etrangere donnes
ADD CONSTRAINT FK_Donnees FOREIGN KEY (noUtilisateur)
        REFERENCES Utilisateurs(noUtilisateur);		
ALTER TABLE Donnees --son positif
ADD CONSTRAINT Check_Son CHECK (valSon >= 0);

ALTER TABLE Calendrier --cle etrangere calendrier utilisateur
ADD CONSTRAINT FK_Calendrier FOREIGN KEY (noUtilisateur)
        REFERENCES Utilisateurs(noUtilisateur);
ALTER TABLE Calendrier --ressenti entre 0 et 10
ADD CONSTRAINT Check_Ressenti CHECK (ressenti BETWEEN 1 AND 10);




