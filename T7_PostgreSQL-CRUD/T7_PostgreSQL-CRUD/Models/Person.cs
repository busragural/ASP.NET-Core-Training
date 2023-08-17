using System;
using System.Collections.Generic;

namespace Tasks.Models;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }
}
