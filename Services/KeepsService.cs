using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {

    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal Keep Create(Keep newKeep)
    {
      newKeep.Id = _repo.Create(newKeep);
      return newKeep;
    }

    internal IEnumerable<Keep> Get()
    {
      return _repo.Get();
      //   return _repo.Get().ToList().FindAll(keep => keep.IsAvailable);
    }

    // internal IEnumerable<Keep> GetKeepsByProfile(string profileId, string userId)
    // {

    //   return _repo.getKeepsByProfile(profileId).ToList().FindAll(keep => keep.CreatorId == userId || keep.IsAvailable);
    // }

    internal string Delete(int id)
    {
      if (_repo.Delete(id))
      {
        return "Deleted!";
      }
      return "Unable to be deleted";
    }

    internal Keep GetOne(int id)
    {
      return _repo.GetOne(id);
    }

    internal Keep Edit(Keep editedKeep, Profile userInfo)
    {
      Keep original = _repo.GetOne(editedKeep.Id);
      if (original == null)
      {
        throw new Exception("Does not exist");
      }
      if (original.CreatorId != userInfo.Id)
      {
        throw new Exception("Access Denied");
      }
      _repo.Edit(editedKeep);
      return _repo.GetOne(editedKeep.Id);
    }
  }
}