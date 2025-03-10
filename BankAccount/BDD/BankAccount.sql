CREATE TABLE Client (
    Id_Client INT PRIMARY KEY AUTOINCREMENT,
    Nom VARCHAR(50),
    Prenom VARCHAR(50),
    Mail VARCHAR(100),
    Tel VARCHAR(10), 
    create_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Comptes (
    Id_Compte INT PRIMARY KEY AUTOINCREMENT, 
    Type VARCHAR(50),
    solde DECIMAL(10,2),
    create_at DATETIME DEFAULT CURRENT_TIMESTAMP
    interet DECIMAL(10,2),
    RIB GUID, 
    Numero_Compte INT UNIQUE, 
    Id_Client INT, 
    FOREIGN KEY (Id_Client) REFERENCES Client(Id_Client)
);

CREATE TABLE Historique (
    Id_Historique INT PRIMARY KEY AUTOINCREMENT,
    Date_Operation DATETIME DEFAULT CURRENT_TIMESTAMP
    Montant DECIMAL(10,2),
    Type_Operation VARCHAR(50),
    Donneur INT,
    Receveur INT,
    FOREIGN KEY (Receveur) REFERENCES Comptes(Id_Compte),
    FOREIGN KEY (Donneur) REFERENCES Comptes(Id_Compte)
);