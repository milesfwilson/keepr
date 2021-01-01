using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProfilesController : ControllerBase
  {

    private readonly ProfilesService _profileService;

    public ProfilesController(ProfilesService profileService)
    {
      _profileService = profileService;
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
  }



}