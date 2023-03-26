using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RPais
{
    public async Task AddRangeAsyn(IEnumerable<Pais> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(Pais model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Paises!.RemoveRange(context.Paises);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<Pais> DoListAsync(Expression<Func<Pais, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.Paises!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}