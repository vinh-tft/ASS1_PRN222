using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public  class Car
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Configuration { get; set; }

    public decimal Price { get; set; }

    public bool IsElectric { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<TestDriveAppointment> TestDriveAppointments { get; set; } = new List<TestDriveAppointment>();
}
