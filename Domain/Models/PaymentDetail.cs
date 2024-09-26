using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class PaymentDetail
{
    public int PaymentId { get; set; }

    public Guid TransactionId { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public int Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool IsConfirmed { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
