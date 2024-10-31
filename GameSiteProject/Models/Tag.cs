using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models;

public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}