USE PROG3A25_Production_AllysonJad;

ALTER TABLE Utilisateurs
ADD CONSTRAINT FK_Utilisateurs FOREIGN KEY (medecinAttitre)
        REFERENCES Utilisateurs(noUtilisateur);
ALTER TABLE Utilisateurs
ADD CONSTRAINT DF_Medecin DEFAULT 0 FOR medecin;
ALTER TABLE Utilisateurs
ADD CONSTRAINT DF_Config DEFAULT 1 FOR config;

ALTER TABLE Donnees
ADD CONSTRAINT FK_Donnees FOREIGN KEY (noUtilisateur)
        REFERENCES Utilisateurs(noUtilisateur);

ALTER TABLE Calendrier
ADD CONSTRAINT FK_Calendrier FOREIGN KEY (noUtilisateur)
        REFERENCES Utilisateurs(noUtilisateur);