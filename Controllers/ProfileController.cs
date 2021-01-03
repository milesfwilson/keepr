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
  public class ProfileController : ControllerBase
  {



    private readonly KeepsService _keepsService;

    public ProfileController(KeepsService keepsService)
    {
      _keepsService = keepsService;
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