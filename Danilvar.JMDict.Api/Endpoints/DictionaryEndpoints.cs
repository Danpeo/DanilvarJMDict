using Danilvar.JMDict.Api.Repositories;
using WanaKanaNet;

namespace Danilvar.JMDict.Api.Endpoints;

public static class DictionaryEndpoints
{
    public static void MapDictionaryEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/v1/Words");
        group.MapGet("/translation/{entry}", ListWordsByTranslationAsync);
        group.MapGet("/kana/{entry}", ListWordsByKanaAsync);
        group.MapGet("/kanji/{entry}", ListWordsByKanjiAsync);
        
    }

    private static async Task<IResult> ListWordsByTranslationAsync(string entry, DictionaryRepository dictRepository)
    {
        if (string.IsNullOrEmpty(entry))
            return Results.BadRequest("Entry is null or empty.");
        
        var words = await dictRepository.ListWordsByTranslationAsync(entry);

        var val = words.ToList();

        if (val.Any())
            return Results.Ok(val);

        return Results.NoContent();
    }

    private static async Task<IResult> ListWordsByKanaAsync(string entry, bool useRomaji,
        DictionaryRepository dictRepository)
    {
        if (string.IsNullOrEmpty(entry))
            return Results.BadRequest("Entry is null or empty.");
        
        if (useRomaji)
            entry = WanaKana.ToKana(entry);

        var words = await dictRepository.ListWordsByKanasAsync(entry);

        var val = words.ToList();

        if (val.Any())
            return Results.Ok(val);

        return Results.NoContent();
    }

    private static async Task<IResult> ListWordsByKanjiAsync(string entry,
        DictionaryRepository dictRepository)
    {
        if (string.IsNullOrEmpty(entry))
            return Results.BadRequest("Entry is null or empty.");
        
        var words = await dictRepository.ListWordsByKanjiAsync(entry);

        var val = words.ToList();

        if (val.Any())
            return Results.Ok(val);

        return Results.NoContent();
    }
}