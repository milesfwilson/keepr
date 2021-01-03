using System;
using System.Collections.Generic;
using System.Linq;
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
      return _repo.Get(profileId).ToList().FindAll(vault => vault.CreatorId == profileId || !vault.IsPrivate);
    }

    internal string Delete(int id, Profile userInfo)
    {

      Vault returnedVault = _repo.GetOne(id);
      if (returnedVault == null)
      {
        throw new Exception("Does not exist");
      }
      if (returnedVault.CreatorId != userInfo.Id)
      {
        throw new Exception("Access Denied");
      }
      if (_repo.Delete(id))
      {
        return "Deleted!";
      }
      else
      {
        return "Failed";
      }

    }

    internal Vault GetOne(int id, Profile userInfo)
    {

      Vault returnedVault = _repo.GetOne(id);
      if (returnedVault == null)
      {
        throw new Exception("Does not exist");
      }
      else if (!returnedVault.IsPrivate || returnedVault.CreatorId == userInfo.Id)
      {
        return returnedVault;
      }
      else
      {
        throw new Exception("Access Denied");
      }
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

    internal IEnumerable<Vault> GetVaultsByProfile(string profileId, string userId)
    {
      return _repo.GetVaultsByProfile(profileId).ToList().FindAll(vault => (vault.CreatorId == profileId && !vault.IsPrivate) || vault.CreatorId == userId);

    }
  }
}