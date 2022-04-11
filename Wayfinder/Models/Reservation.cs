using System;
using System.Collections.Generic;

namespace Wayfinder.Models
{
  public class Reservation : Virtual<int>
  {
    public string Type { get; set; }
    public string Name { get; set; }
    public string ConfirmNum { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public int Cost { get; set; }
    public int TripId { get; set; }

  }
}