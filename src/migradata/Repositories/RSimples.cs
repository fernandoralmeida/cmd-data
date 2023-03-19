using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RSimples
{
    public async Task AddRangeAsyn(IEnumerable<Simples> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(Simples model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Simples!.RemoveRange(context.Simples);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<Simples> DoListAsync(Expression<Func<Simples, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.Simples!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}