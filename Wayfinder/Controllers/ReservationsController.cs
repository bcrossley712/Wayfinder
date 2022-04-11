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
  public class ReservationsController : ControllerBase
  {
    private readonly ReservationsService _resService;

    public ReservationsController(ReservationsService resService)
    {
      _resService = resService;
    }
    [HttpGet]
    public ActionResult<List<Reservation>> GetAll()
    {
      try
      {
        List<Reservation> reservations = _resService.GetAll();
        return Ok(reservations);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Reservation> GetById(int id)
    {
      try
      {
        Reservation reservation = _resService.GetById(id);
        return Ok(reservation);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Reservation>> Create(int id, [FromBody] Reservation reservationData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        reservationData.Id = id;
        Reservation reservation = _resService.Create(userInfo.Id, reservationData);
        return Created($"api/reservations/{reservation.Id}", reservation);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Reservation>> Edit(int id, [FromBody] Reservation updateData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        Reservation update = _resService.Edit(userInfo.Id, updateData);
        return Created($"api/reservations/{update.Id}", update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}