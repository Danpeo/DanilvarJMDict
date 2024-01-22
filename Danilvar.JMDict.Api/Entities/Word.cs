using System;
using System.Collections.Generic;

namespace Danilvar.JMDict.Api.Entities;

public partial class Word
{
    public string Id { get; set; } = null!;

    public Guid? JmdictDataId { get; set; }

    public virtual JmdictData? JmdictData { get; set; }

    public virtual ICollection<Kana> Kanas { get; set; } = new List<Kana>();

    public virtual ICollection<Kanji> Kanjis { get; set; } = new List<Kanji>();

    public virtual ICollection<Sense> Senses { get; set; } = new List<Sense>();
}
