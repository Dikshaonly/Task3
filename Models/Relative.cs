using System;
using System.Collections.Generic;

namespace Task3.Models;

public partial class Relative
{
    public int Rid { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Relation { get; set; }

    public int? Eid { get; set; }

    public string? Gender { get; set; }

    public virtual Employee? EidNavigation { get; set; }
}