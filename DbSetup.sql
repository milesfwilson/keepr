-- CREATE TABLE profiles (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     picture VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id)
-- )


    -- CREATE TABLE vaults (
    --     name VARCHAR(255) NOT NULL,
    --     creatorId VARCHAR(255) NOT NULL,
    --     id INT NOT NULL AUTO_INCREMENT,
    --     isPrivate TINYINT NOT NULL,
    --     description VARCHAR(255) NOT NULL,
    --     PRIMARY KEY (id),
    --         FOREIGN KEY (creatorId)
    --         REFERENCES profiles(id)
    --         ON DELETE CASCADE
    -- )

    -- CREATE TABLE keeps (
    --   id INT NOT NULL AUTO_INCREMENT, 
    --   creatorId VARCHAR(255) NOT NULL, 
    --   name VARCHAR(255) NOT NULL, 
    --   description VARCHAR(255) NOT NULL, 
    --   img VARCHAR(255) NOT NULL, 
    --   views INT NOT NULL, 
    --   shares INT NOT NULL, 
    --   keeps INT NOT NULL, 
    --   tags VARCHAR(255),
    --     PRIMARY KEY (id),
    --     FOREIGN KEY (creatorId)
    --     REFERENCES profiles(id)
    --     ON DELETE CASCADE
    -- )

    -- ALTER TABLE keeps MODIFY tags VARCHAR(255);

--     CREATE TABLE vaultKeeps (
-- id INT NOT NULL AUTO_INCREMENT,
-- vaultId INT NOT NULL,
-- keepId INT NOT NULL,
-- creatorId VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
    
--     FOREIGN KEY(vaultId)
--     REFERENCES vaults (id)
--     ON DELETE CASCADE,

--     FOREIGN KEY(keepId)
--     REFERENCES keeps (id)
--     ON DELETE CASCADE,
    
--     FOREIGN KEY(creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE

--     )