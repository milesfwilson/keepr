using System;
using System.Collections.Generic;
using System.Data;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {

    private readonly IDbConnection _db;

    private readonly string populateCreator = "SELECT vaultKeep.*, profile.* FROM vaultKeeps vaultKeep INNER JOIN profiles profile ON vaultKeep.creatorId = profile.id ";
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(VaultKeep newVaultKeep)
    {

      string sql = @"INSERT INTO vaultKeeps
            (vaultId, keepId, creatorId, id)
            VALUES
            (@VaultId, @KeepId, @creatorId, @id);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }

    internal IEnumerable<VaultKeep> Get()
    {
      string sql = "SELECT * FROM vaultKeeps";
      return _db.Query<VaultKeep>(sql);
    }

    internal void Edit(VaultKeep editedVaultKeep)
    {
      string sql = @"
      UPDATE vaultKeeps
      SET
      vaultId = @VaultId,
      keepId = @KeepId
WHERE id = @Id;";
      _db.Execute(sql, editedVaultKeep);
    }

    internal bool Delete(int id)
    {
      string sql = @"DELETE FROM vaultKeeps WHERE id = @Id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }

    // internal IEnumerable<Item> GetItemsByListId(int listId)
    // {
    //   string sql = @"
    //     SELECT item.*,
    //     listitem.id as VaultKeepId,
    //     profile.*
    //     FROM listitems listitem
    //     JOIN items item ON item.id = listitem.itemId
    //     JOIN profiles profile ON profile.id = item.creatorId
    //     WHERE listId = @ListId;
    //     ";
    //   return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (item, profile) => { item.Creator = profile; return item; }, new { listId }, splitOn: "id");

    // }

    internal VaultKeep GetOne(int id)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}