using System.Collections.Generic;

namespace Wayfinder.Interfaces
{
  public interface IRepository<T, Tid>
  {
    List<T> GetAll();
    T GetById(Tid id);
    T Create(T data);
    T Edit(T data);
    string Delete(Tid id);
  }
}