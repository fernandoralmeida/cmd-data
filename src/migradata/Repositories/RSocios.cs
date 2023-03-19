using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RSocios
{
    public async Task AddRangeAsyn(IEnumerable<Socio> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(Socio model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Socios!.RemoveRange(context.Socios);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<Socio> DoListAsync(Expression<Func<Socio, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.Socios!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}