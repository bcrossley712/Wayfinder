using System.Collections.Generic;
using Wayfinder.Models;
using Wayfinder.Interfaces;
using System.Data;
using Dapper;
using System.Linq;

namespace Wayfinder.Repositories
{
  public class TripsRepository : IRepository<Trip, int>
  {
    private IDbConnection _db;

    public TripsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Trip> GetAll()
    {
      string sql = @"
      SELECT 
       a.*,
       t.*
      FROM trips t
      JOIN accounts a 
      WHERE a.id = t.creatorId
      ";
      return _db.Query<Trip, Account, Trip>(sql, (trip, account) =>
      {
        trip.Creator = account;
        return trip;
      }).ToList();
    }

    public Trip GetById(int id)
    {
      string sql = @"
      SELECT 
      t.*,
      a.*
      FROM trips t 
      JOIN accounts a ON t.creatorId = a.id
      WHERE t.id = @id";
      return _db.Query<Trip, Account, Trip>(sql, (trip, account) =>
      {
        trip.Creator = account;
        return trip;
      }, new { id }).FirstOrDefault();
    }
    public Trip Create(Trip data)
    {
      string sql = @"
      INSERT INTO trips
      (title, creatorId)
      VALUES
      (@Title, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data; ;
    }

    public Trip Edit(Trip data)
    {
      string sql = @"
      UPDATE trips
      SET
      title = @Title
      WHERE id = @Id;";
      _db.Execute(sql, data);
      return GetById(data.Id);
    }
    public void Delete(int id)
    {
      string sql = "DELETE FROM trips WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

  }
}