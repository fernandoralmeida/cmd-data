using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RMotivoSituacaoCadastral
{
    public async Task AddRangeAsyn(IEnumerable<MotivoSituacaoCadastral> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(MotivoSituacaoCadastral model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.MotivoSituacaoCadastral!.RemoveRange(context.MotivoSituacaoCadastral);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<MotivoSituacaoCadastral> DoListAsync(Expression<Func<MotivoSituacaoCadastral, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.MotivoSituacaoCadastral!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}