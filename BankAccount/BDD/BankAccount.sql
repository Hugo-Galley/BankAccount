CREATE TABLE Client (
    Id_Client INT PRIMARY KEY AUTO_INCREMENT,
    Nom VARCHAR(50) NOT NULL,
    Prenom VARCHAR(50) NOT NULL,
    Mail VARCHAR(100) NOT NULL UNIQUE,
    Tel VARCHAR(15),
    mdp TEXT NOT NULL,
    create_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Comptes (
    Id_Compte INT PRIMARY KEY AUTO_INCREMENT, 
    Type VARCHAR(50) NOT NULL,
    solde DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    create_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    interet DECIMAL(10,2) DEFAULT 0.00,
    RIB CHAR(36) UNIQUE, 
    Numero_Compte VARCHAR(20) UNIQUE, 
    Id_Client INT NOT NULL, 
    FOREIGN KEY (Id_Client) REFERENCES Client(Id_Client) ON DELETE CASCADE
);

CREATE TABLE Historique (
    Id_Historique INT PRIMARY KEY AUTO_INCREMENT,
    Date_Operation DATETIME DEFAULT CURRENT_TIMESTAMP,
    Montant DECIMAL(10,2) NOT NULL,
    Type_Operation VARCHAR(50) NOT NULL,
    Donneur INT NOT NULL,
    Receveur INT NOT NULL,
    FOREIGN KEY (Receveur) REFERENCES Comptes(Id_Compte) ON DELETE CASCADE,
    FOREIGN KEY (Donneur) REFERENCES Comptes(Id_Compte) ON DELETE CASCADE
);