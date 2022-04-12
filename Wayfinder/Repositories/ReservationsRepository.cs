using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
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

    public List<Reservation> GetAll()
    {
      string sql = @"
      SELECT 
       t.*,
       r.*
      FROM reservations r
      JOIN trips t 
      WHERE t.id = r.tripId
      ";
      return _db.Query<Reservation, Trip, Reservation>(sql, (reservation, trip) =>
      {
        reservation.Trip = trip;
        return reservation;
      }).ToList();
    }

    public Reservation GetById(int id)
    {
      string sql = @"
      SELECT 
      r.*,
      t.*
      FROM reservations r 
      JOIN trips t ON r.tripId = t.id
      WHERE r.id = @id";
      return _db.Query<Reservation, Trip, Reservation>(sql, (reservation, trip) =>
      {
        reservation.Trip = trip;
        return reservation;
      }, new { id }).FirstOrDefault();
    }
    public Reservation Create(Reservation data)
    {
      string sql = @"
      INSERT INTO reservations
      (type, name, confirmNum, address, date, cost)
      VALUES
      (@Type, @Name, @ConfirmNum, @Address, @Date, @Cost);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data; ;
    }

    public Reservation Edit(Reservation data)
    {
      string sql = @"
      UPDATE reservations
      SET
      type = @Type,
      name = @Name, 
      confirmNum = @ConfirmNum, 
      address = @Address, 
      date = @Date, 
      cost = @Cost
      WHERE id = @Id;";
      _db.Execute(sql, data);
      return GetById(data.Id);
    }
    public void Delete(int id)
    {
      string sql = "DELETE FROM reservations WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

  }
}