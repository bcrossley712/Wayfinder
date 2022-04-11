using System.Collections.Generic;

namespace Wayfinder.Interfaces
{
  public interface IServices<T, Tid>
  {
    List<T> GetAll();
    T GetById(Tid id);
    T Create(T data);
    T Edit(T data);
    string Delete(Tid id);

  }
}