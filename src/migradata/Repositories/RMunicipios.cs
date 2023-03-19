using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RMunicipios
{
    public async Task AddRangeAsyn(IEnumerable<Municipio> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(Municipio model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Municipios!.RemoveRange(context.Municipios);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<Municipio> DoListAsync(Expression<Func<Municipio, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.Municipios!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}