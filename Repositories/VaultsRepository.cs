using System;
using System.Collections.Generic;
using System.Data;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
  public class VaultsRepository
  {

    private readonly IDbConnection _db;

    private readonly string populateCreator = "SELECT vault.*, profile.* FROM vaults vault INNER JOIN profiles profile ON vault.creatorId = profile.id ";
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(Vault newVault)
    {

      string sql = @"INSERT INTO vaults
            (name, id, isPrivate, description, creatorId)
            VALUES
            (@Name, @Id, @IsPrivate, @Description, @CreatorId);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVault);
    }

    internal IEnumerable<Vault> Get(string profileId)
    {
      string sql = populateCreator;
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { profileId }, splitOn: "id");
    }

    internal void Edit(Vault editedVault)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
WHERE id = @Id;";
      _db.Execute(sql, editedVault);
    }

    internal IEnumerable<Vault> GetVaultsByProfile(string profileId)
    {
      string sql = @"
                SELECT
                vault.*,
                profile.*
                FROM vaults vault
                JOIN profiles profile ON
                vault.creatorId = profile.id;
";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { profileId }, splitOn: "id");
    }

    internal bool Delete(int id)
    {
      string sql = @"DELETE FROM vaults WHERE id = @Id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }

    internal Vault GetOne(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }
  }
}