using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? MovieName { get; set; }

    public string? Synopsis { get; set; }

    public string? Director { get; set; }

    public string? Duration { get; set; }

    public string? Genre { get; set; }

    public int? Rating { get; set; }

    public string? MovieImage { get; set; }

    public int MoviePrice { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();
}
