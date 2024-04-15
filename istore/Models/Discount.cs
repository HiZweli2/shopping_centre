using System;
using System.Collections.Generic;

namespace istore.Models;

public partial class Discount
{
    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
