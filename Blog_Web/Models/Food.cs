using System;
using System.Collections.Generic;

namespace Blog_Web.Models;

public partial class Food
{
    public long Foodid { get; set; }

    public string? FoodName { get; set; }

    public decimal? Price { get; set; }

    public string? PhotoUrl { get; set; }
}
