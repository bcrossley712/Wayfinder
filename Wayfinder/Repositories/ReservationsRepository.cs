using System;
using System.Collections.Generic;
using System.Data;
using Wayfinder.Interfaces;
using Wayfinder.Models;

namespace Wayfinder.Repositories
{
  public class ReservationsRepository : IRepository<Reservation, int>
  {
    private readonly IDbConnection _db;

    public ReservationsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Reservation Create(Reservation data)
    {
      throw new NotImplementedException();
    }

    public string Delete(int id)
    {
      throw new NotImplementedException();
    }

    public Reservation Edit(Reservation data)
    {
      throw new NotImplementedException();
    }

    public List<Reservation> GetAll()
    {
      throw new NotImplementedException();
    }

    public Reservation GetById(int id)
    {
      throw new NotImplementedException();
    }
  }
}