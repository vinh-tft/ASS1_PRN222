using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class TestDriveAppointment
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int CarId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
