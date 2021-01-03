using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {

    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetOrCreateProfile(Profile userInfo)
    {
      Profile foundProfile = _repo.GetByEmail(userInfo.Email);
      if (foundProfile == null)
      {
        return _repo.Create(userInfo);
      }
      return foundProfile;
    }

    internal object GetProfileById(string profileId)
    {
      Profile foundProfile = _repo.GetProfileById(profileId);
      if (foundProfile == null)
      {
        throw new Exception("Profile does not exist");
      }
      return foundProfile;
    }
  }
}