using System.Collections.Generic;
using Wayfinder.Interfaces;
using Wayfinder.Models;
using Wayfinder.Repositories;
using System;

namespace Wayfinder.Services
{
  public class TripsService : IServices<Trip>
  {
    private TripsRepository _tripRepo;

    public TripsService(TripsRepository tripRepo)
    {
      _tripRepo = tripRepo;
    }

    public List<Trip> GetAll()
    {
      return _tripRepo.GetAll();
    }

    public Trip GetById(int id)
    {
      Trip found = _tripRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Trip Id");
      }
      return found;
    }
    public Trip Create(string userId, Trip data)
    {
      data.CreatorId = userId;
      return _tripRepo.Create(data);
    }

    public Trip Edit(string userId, Trip data)
    {
      Trip original = GetById(data.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You cannot edit this trip");
      }
      original.Title = data.Title ?? original.Title;
      Trip update = _tripRepo.Edit(data);
      original.UpdatedAt = update.UpdatedAt;
      return original;
    }
    public void Delete(string userId, int id)
    {
      Trip found = GetById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("You cannot delete this trip");
      }
      _tripRepo.Delete(id);
    }

  }
}