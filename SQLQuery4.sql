CREATE TABLE Notifications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    Message NVARCHAR(255),
    IsSent BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
