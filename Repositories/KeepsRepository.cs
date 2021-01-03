using System;
using System.Collections.Generic;
using System.Data;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
  public class KeepsRepository
  {

    private readonly IDbConnection _db;

    private readonly string populateCreator = "SELECT keep.*, profile.* FROM keeps keep INNER JOIN profiles profile ON keep.creatorId = profile.id ";
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(Keep newKeep)
    {
      newKeep.Keeps = 0;
      newKeep.Shares = 0;
      newKeep.Views = 0;
      string sql = @"INSERT INTO keeps
            (id, creatorId, name, description, img, views, shares, keeps, tags)
            VALUES
            (@Id, @CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps, @Tags);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }

    internal IEnumerable<Keep> GetKeepsByProfile(string profileId)
    {
      string sql = @"
            SELECT 
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id;
";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { profileId }, splitOn: "id");
    }

    // internal IEnumerable<Keep> getKeepsByProfile(string profileId)
    // {
    //   string sql = @"
    //             SELECT
    //             keep.*,
    //             profile.*
    //             FROM keeps keep
    //             JOIN profiles profile ON keep.creatorId = profile.id;";
    //   return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { profileId }, splitOn: "id");
    // }

    internal IEnumerable<Keep> Get()
    {
      string sql = populateCreator;
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

    internal void Edit(Keep editedKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
description = @Description,
img = @Img,
views = @Views,
shares = @Shares,
keeps = @Keeps,
tags = @Tags
WHERE id = @Id;";
      _db.Execute(sql, editedKeep);
    }

    internal bool Delete(int id)
    {
      string sql = @"DELETE FROM keeps WHERE id = @Id LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows > 0;
    }

    internal Keep GetOne(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }
  }
}