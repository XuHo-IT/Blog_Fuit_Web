using System;
using System.Collections.Generic;

namespace Blog_Web.Models;

public partial class Info
{
    public long Infoid { get; set; }

    public string? Infoname { get; set; }

    public string? PhotoUrl { get; set; }

    public string? Position { get; set; }

    public string? Address { get; set; }

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }
}
