using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wayfinder.Models;
using Wayfinder.Services;

namespace Wayfinder.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TripsController : ControllerBase
  {
    private readonly TripsService _resService;

    public TripsController(TripsService resService)
    {
      _resService = resService;
    }
    [HttpGet]
    public ActionResult<List<Trip>> GetAll()
    {
      try
      {
        List<Trip> trips = _resService.GetAll();
        return Ok(trips);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Trip> GetById(int id)
    {
      try
      {
        Trip trip = _resService.GetById(id);
        return Ok(trip);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Trip>> Create([FromBody] Trip tripData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Trip trip = _resService.Create(userInfo.Id, tripData);
        return Created($"api/trips/{trip.Id}", trip);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Trip>> Edit(int id, [FromBody] Trip updateData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        Trip update = _resService.Edit(userInfo.Id, updateData);
        return Created($"api/trips/{update.Id}", update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _resService.Delete(userInfo.Id, id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}