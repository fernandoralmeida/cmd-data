using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RCnaes
{
    public async Task AddRangeAsyn(IEnumerable<Cnae> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(Cnae model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Cnaes!.RemoveRange(context.Cnaes);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<Cnae> DoListAsync(Expression<Func<Cnae, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.Cnaes!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}