using System.Collections.Generic;

namespace Wayfinder.Interfaces
{
  public interface IServices<T>
  {
    List<T> GetAll();
    T GetById(int id);
    T Create(string userId, T data);
    T Edit(string userId, T data);
    void Delete(string userId, int id);

  }
}