using System;
using System.Collections.Generic;

namespace Danilvar.JMDict.Api.Entities;

public partial class Gloss
{
    public Guid Id { get; set; }

    public string? Lang { get; set; }

    public string? Gender { get; set; }

    public string? Type { get; set; }

    public string? Text { get; set; }

    public Guid? SenseId { get; set; }

    //public virtual Sense? Sense { get; set; }
}
