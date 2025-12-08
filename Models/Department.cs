using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task3.Models;

public partial class Department
{
    public int DepId { get; set; }
    [Required]
    public string? DepName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}