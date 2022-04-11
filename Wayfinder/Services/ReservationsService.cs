using Wayfinder.Interfaces;
using Wayfinder.Models;
using Wayfinder.Repositories;

namespace Wayfinder.Services
{
  public class ReservationsService : IServices<Reservation, int>
  {
    private readonly ReservationsRepository _resRepo;

    public ReservationsService(ReservationsRepository resRepo)
    {
      _resRepo = resRepo;
    }

    public Reservation Create(Reservation data)
    {
      throw new System.NotImplementedException();
    }

    public string Delete(int id)
    {
      throw new System.NotImplementedException();
    }

    public Reservation Edit(Reservation data)
    {
      throw new System.NotImplementedException();
    }

    public System.Collections.Generic.List<Reservation> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public Reservation GetById(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}