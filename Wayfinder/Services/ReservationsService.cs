using System;
using System.Collections.Generic;
using Wayfinder.Interfaces;
using Wayfinder.Models;
using Wayfinder.Repositories;

namespace Wayfinder.Services
{
  public class ReservationsService : IServices<Reservation>
  {
    private readonly ReservationsRepository _resRepo;
    private readonly TripsService _tripsService;

    public ReservationsService(ReservationsRepository resRepo, TripsService tripsService)
    {
      _resRepo = resRepo;
      _tripsService = tripsService;
    }

    public List<Reservation> GetAll()
    {
      return _resRepo.GetAll();
    }

    public Reservation GetById(int id)
    {
      Reservation found = _resRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Reservation Id");
      }
      return found;
    }
    public Reservation Create(string userId, Reservation data)
    {
      Trip foundTrip = _tripsService.GetById(data.TripId);
      foundTrip.CreatorId = userId;
      return _resRepo.Create(data);
    }

    public Reservation Edit(string userId, Reservation data)
    {
      Trip foundTrip = _tripsService.GetById(data.TripId);
      Reservation original = GetById(data.Id);
      if (foundTrip.CreatorId != userId)
      {
        throw new Exception("You cannot edit reservations on this trip");
      }
      original.Name = data.Name ?? original.Name;
      original.Address = data.Address ?? original.Address;
      original.Type = data.Type ?? original.Type;
      original.ConfirmNum = data.ConfirmNum ?? original.ConfirmNum;
      original.Date = data.Date != null ? data.Date : original.Date;
      original.Cost = data.Cost >= 0 ? data.Cost : original.Cost;
      Reservation update = _resRepo.Edit(data);
      original.UpdatedAt = update.UpdatedAt;
      return original;
    }
    public void Delete(string userId, int id)
    {
      Reservation found = GetById(id);
      Trip foundTrip = _tripsService.GetById(found.TripId);
      if (foundTrip.CreatorId != userId)
      {
        throw new Exception("You cannot delete reservations on this trip");
      }
      _resRepo.Delete(id);
    }
  }
}