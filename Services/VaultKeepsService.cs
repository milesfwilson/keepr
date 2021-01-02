using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      newVaultKeep.Id = _repo.Create(newVaultKeep);
      return newVaultKeep;
    }

    internal IEnumerable<VaultKeep> Get()
    {
      return _repo.Get();
    }

    internal string Delete(int id)
    {
      if (_repo.Delete(id))
      {
        return "Deleted!";
      }
      return "Unable to be deleted";
    }

    internal VaultKeep GetOne(int id)
    {
      return _repo.GetOne(id);
    }

    internal VaultKeep Edit(VaultKeep editedVaultKeep, Profile userInfo)
    {
      VaultKeep original = _repo.GetOne(editedVaultKeep.Id);
      if (original == null)
      {
        throw new Exception("Does not exist");
      }
      if (original.CreatorId != userInfo.Id)
      {
        throw new Exception("Access Denied");
      }
      _repo.Edit(editedVaultKeep);
      return _repo.GetOne(editedVaultKeep.Id);
    }

    // internal object GetItemsByListId(int id)
    // {
    //   return _repo.GetItemsByListId(id);
    // }
  }
}