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
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService)
    {
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
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
    [Authorize]

    public async Task<ActionResult<Vault>> GetOne(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vaultsService.GetOne(id, userInfo));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }
    [HttpGet("{id}/keeps")]

    public ActionResult<IEnumerable<Vault>> GetKeepsByVaultId(int id)
    {
      try
      {
        return Ok(_vaultKeepsService.GetKeepsByVaultId(id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

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
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vaultsService.Delete(id, userInfo));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }
  }
}