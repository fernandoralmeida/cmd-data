using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class REstabelecimentos
{
    public async Task AddRangeAsyn(IEnumerable<Estabelecimento> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(Estabelecimento model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Estabelecimentos!.RemoveRange(context.Estabelecimentos);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<Estabelecimento> DoListAsync(Expression<Func<Estabelecimento, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.Estabelecimentos!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}