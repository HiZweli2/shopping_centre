using System;
using System.Collections.Generic;

namespace istore.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Stock { get; set; }

    public string? DiscountName { get; set; }

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public bool Isdeleted { get; set; }

    public virtual Discount? DiscountNameNavigation { get; set; }
}
