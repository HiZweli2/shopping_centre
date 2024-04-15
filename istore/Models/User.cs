using System;
using System.Collections.Generic;

namespace istore.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? DiscountName { get; set; }

    public string? City { get; set; }

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Discount? DiscountNameNavigation { get; set; }
}
