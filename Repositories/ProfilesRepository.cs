

using System;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class ProfilesRepository
  {

    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetByEmail(string email)
    {
      string sql = "SELECT * FROM profiles WHERE email = @Email";
      return _db.QueryFirstOrDefault<Profile>(sql, new { email });
    }

    internal Profile Create(Profile userInfo)
    {
      string sql = @"
                INSERT INTO profiles
                (id, name, email, picture)
                VALUES
                (@Id, @Name, @Email, @Picture)
            ";
      _db.Execute(sql, userInfo);
      return userInfo;
    }

    internal Profile GetProfileById(string profileId)
    {
      string sql = "SELECT * FROM profiles WHERE id = @ProfileId";
      return _db.QueryFirstOrDefault<Profile>(sql, new { profileId });
    }
  }
}