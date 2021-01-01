using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {

    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal Vault Create(Vault newVault)
    {
      newVault.Id = _repo.Create(newVault);
      return newVault;
    }

    internal IEnumerable<Vault> Get(string profileId)
    {
      return _repo.Get(profileId);
    }

    internal string Delete(int id)
    {
      if (_repo.Delete(id))
      {
        return "Deleted!";
      }
      return "Unable to be deleted";
    }

    internal Vault GetOne(int id)
    {
      return _repo.GetOne(id);
    }

    internal Vault Edit(Vault editedVault, Profile userInfo)
    {
      Vault original = _repo.GetOne(editedVault.Id);
      if (original == null)
      {
        throw new Exception("Does not exist");
      }
      if (original.CreatorId != userInfo.Id)
      {
        throw new Exception("Access Denied");
      }
      _repo.Edit(editedVault);
      return _repo.GetOne(editedVault.Id);
    }
  }
}