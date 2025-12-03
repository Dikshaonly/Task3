using System;
using System.Collections.Generic;

namespace Task3.Models;

public partial class Department
{
    public int DepId { get; set; }

    public string? DepName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}