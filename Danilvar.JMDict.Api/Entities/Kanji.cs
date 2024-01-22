using System;
using System.Collections.Generic;

namespace Danilvar.JMDict.Api.Entities;

public partial class Kanji
{
    public Guid Id { get; set; }

    public bool? Common { get; set; }

    public string? Text { get; set; }

    public List<string>? Tags { get; set; }

    public string? WordId { get; set; }

    //public virtual Word? Word { get; set; }
}
