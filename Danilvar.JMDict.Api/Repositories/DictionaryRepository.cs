using Danilvar.JMDict.Api.Context;
using Danilvar.JMDict.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Danilvar.JMDict.Api.Repositories;

public class DictionaryRepository(AppDbContext context)
{
    public async Task<IEnumerable<Word>> ListWordsByTranslationAsync(string entry)
    {
        var result = await GetWords()
            .Where(w => w.Senses
                .Any(s => s.Glosses
                    .Any(g => EF.Functions.ILike(g.Text, $"%{entry}%"))))
            .OrderByDescending(w => w.Senses
                .SelectMany(s => s.Glosses)
                .Count(g => EF.Functions.ILike(g.Text, entry)))
            .ThenByDescending(w => w.Kanas
                .Select(k => k.Common)
                .Count(x => x.Value == true))
            .ThenByDescending(w => w.Kanjis
                .Select(k => k.Common)
                .Count(x => x.Value == true)
            )
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Word>> ListWordsByKanasAsync(string entry)
    {
        var result = await GetWords()
            .Where(w => w.Kanas
                .Any(k => EF.Functions.ILike(k.Text, $"%{entry}%")))
            .OrderByDescending(w => w.Kanas
                .Count(g => EF.Functions.ILike(g.Text, entry)))
            .ThenByDescending(w => w.Kanas
                .Select(k => k.Common)
                .Count(x => x.Value == true))
            .ThenByDescending(w => w.Kanjis
                .Select(k => k.Common)
                .Count(x => x.Value == true)
            )
            .ToListAsync();
        return result;
    }
    
    public async Task<IEnumerable<Word>> ListWordsByKanjiAsync(string entry)
    {
        var result = await GetWords()
            .Where(w => w.Kanjis
                .Any(k => EF.Functions.ILike(k.Text, $"%{entry}%")))
            .OrderByDescending(w => w.Kanjis
                .Count(g => EF.Functions.ILike(g.Text, entry)))
            .ThenByDescending(w => w.Kanas
                .Select(k => k.Common)
                .Count(x => x.Value == true))
            .ThenByDescending(w => w.Kanjis
                .Select(k => k.Common)
                .Count(x => x.Value == true)
            )
            .ToListAsync();
        return result;
    }

    private IIncludableQueryable<Word, ICollection<Gloss>> GetWords()
    {
        return context.Words
            .AsSplitQuery()
            .Include(w => w.Kanjis)
            .Include(w => w.Kanas)
            .Include(w => w.Senses)
            .ThenInclude(s => s.Glosses);
    }
}