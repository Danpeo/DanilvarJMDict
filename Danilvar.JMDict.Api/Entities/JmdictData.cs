using System;
using System.Collections.Generic;

namespace Danilvar.JMDict.Api.Entities;

public partial class JmdictData
{
    public Guid Id { get; set; }

    public string? Version { get; set; }

    public List<string>? Languages { get; set; }

    public bool? CommonOnly { get; set; }

    public List<string>? DictRevisions { get; set; }

    public Dictionary<string, string>? Tags { get; set; }

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
