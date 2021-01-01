using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Repositories;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;

    public VaultsController(VaultsService vaultsService)
    {
      _vaultsService = vaultsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = userInfo.Id;
        Vault created = _vaultsService.Create(newVault);
        newVault.Creator = userInfo;
        return Ok(created);
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }

    }
    [HttpGet]

    public async Task<ActionResult<Vault>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

        return Ok(_vaultsService.Get(userInfo.Id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<IEnumerable<Vault>> GetOne(int id)
    {
      try
      {
        return Ok(_vaultsService.GetOne(id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }
    // [HttpGet("{id}/Vaultsitem")]

    // public ActionResult<IEnumerable<Vaults>> GetItemsByVaultsId(int id)
    // {
    //   try
    //   {
    //     return Ok(_VaultsItemService.GetItemsByVaultsId(id));
    //   }
    //   catch (System.Exception error)
    //   {
    //     return BadRequest(error.Message);
    //   }
    // }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault editedVaults)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editedVaults.Id = id;
        return Ok(_vaultsService.Edit(editedVaults, userInfo));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }


    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        return Ok(_vaultsService.Delete(id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }
  }
}