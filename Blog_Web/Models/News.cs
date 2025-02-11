using System;
using System.Collections.Generic;

namespace Blog_Web.Models;

public partial class News
{
    public long Newsid { get; set; }

    public string? Author { get; set; }

    public DateTime? DateUpload { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? PhotoUrl { get; set; }
}
