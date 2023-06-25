using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models;

public record TodoCreatedModel 
{
    public TodoCreatedModel(string title)
    {
        Title = title;
    }

    public string Title { get; init; }
}
