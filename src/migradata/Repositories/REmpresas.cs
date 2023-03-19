using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class REmpresas
{
    public async Task AddRangeAsyn(IEnumerable<Empresa> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync()
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Empresas!.RemoveRange(context.Empresas);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<Empresa> DoListAsync(Expression<Func<Empresa, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.Empresas!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter);

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}