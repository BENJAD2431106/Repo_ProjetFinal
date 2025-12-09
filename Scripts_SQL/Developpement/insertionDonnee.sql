USE PROG3A25_AllysonJad;

-- Test rappel 
--INSERT INTO Utilisateurs (nomUtilisateur, prenomUtilisateur, courriel, motDePasse, medecin, age, assuranceSociale)
--VALUES ('Lebeau', 'Normand', 'normand@gmail.con', '123normand', 0, 14, 000011113);

-- Générer par IA 
INSERT INTO Utilisateurs (nomUtilisateur, prenomUtilisateur, courriel, motDePasse, medecin, age, photo, dateRdv, assuranceSociale, config)
VALUES 
('Dupont', 'Alice', 'alice.dupont@email.com', 'MotDePasse123', 0, 28, NULL, '2025-10-01', '123456789', 1),
('Martin', 'Jean', 'jean.martin@email.com', 'PassWord456', 1, 45, NULL, '2025-09-20', '987654321', 0),
('Moreau', 'Claire', 'claire.moreau@email.com', 'Secret789', 0, 32, NULL, NULL, '112233445', 1),
('Lefebvre', 'Marc', 'marc.lefebvre@email.com', 'Azerty123', 0, 50, NULL, '2025-09-25', '556677889', 0);

UPDATE Utilisateurs
SET medecinAttitre = 2
WHERE nomUtilisateur IN ('Dupont','Moreau');

UPDATE Utilisateurs
SET medecinAttitre = NULL
WHERE nomUtilisateur IN ('Martin','Lefebvre');

SELECT * 
FROM Utilisateurs;

-- Générer par IA 
INSERT INTO Donnees (valTemperature, valSon, dateHeure, noUtilisateur)
VALUES 
(22.5, 55.2, '2025-09-18 08:30:00', 1),
(19.8, 60.0, '2025-09-18 12:45:00', 3),
(21.0, 50.5, '2025-09-18 18:15:00', 4);

SELECT *
FROM Donnees

INSERT INTO Calendrier (heureCoucher, heureLever, dates, noUtilisateur, ressenti, nbreReveil, commentaire)
VALUES 
('2025-09-17 23:00:00', '2025-09-18 07:00:00', '2025-09-17', 1, 8, 1, 'Bonne nuit, réveil facile'),
('2025-09-17 22:30:00', '2025-09-18 06:45:00', '2025-09-17', 3, 7, 2, 'Réveils fréquents, sommeil léger');

SELECT *
FROM Calendrier