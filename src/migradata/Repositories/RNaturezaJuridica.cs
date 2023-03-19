using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RNaturezaJuridica
{
    public async Task AddRangeAsyn(IEnumerable<NaturezaJuridica> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(NaturezaJuridica model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.NaturezaJuridica!.RemoveRange(context.NaturezaJuridica);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<NaturezaJuridica> DoListAsync(Expression<Func<NaturezaJuridica, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.NaturezaJuridica!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}