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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.CreatorId = userInfo.Id;
        Keep created = _keepsService.Create(newKeep);
        created.Creator = userInfo;
        return Ok(created);
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }

    }
    [HttpGet]

    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_keepsService.Get());
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<IEnumerable<Keep>> GetOne(int id)
    {
      try
      {

        return Ok(_keepsService.GetOne(id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    // [HttpGet("{id}/option")]
    // public ActionResult<IEnumerable<Option>> GetOptionsByKeepId(int id)
    // {
    //   try
    //   {
    //     return Ok(_optionService.GetOptionsByKeepId(id));
    //   }
    //   catch (System.Exception error)
    //   {

    //     return BadRequest(error.Message);
    //   }
    // }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep editedKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editedKeep.Id = id;
        return Ok(_keepsService.Edit(editedKeep, userInfo));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        return Ok(_keepsService.Delete(id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }
  }
}