using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {

    private readonly ProfilesService _profileService;
    private readonly KeepsService _keepsService;

    private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profileService, KeepsService keepsService, VaultsService vaultsService)
    {
      _profileService = profileService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [HttpGet]
    [Authorize]

    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_profileService.GetOrCreateProfile(userInfo));
      }
      catch (System.Exception error)
      {

        return BadRequest(error.Message);
      }
    }

    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<Profile>> GetVaultsByProfile(string profileId)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vaultsService.GetVaultsByProfile(profileId, userInfo?.Id));

      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }

    }


    [HttpGet("{profileId}/keeps")]
    public async Task<ActionResult<Profile>> GetKeepsByProfile(string profileId)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_keepsService.GetKeepsByProfile(profileId, userInfo?.Id));

      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

  }



}