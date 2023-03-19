using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using migradata.Models;

namespace migradata.Repositories;

public class RQualificacaoSocios
{
    public async Task AddRangeAsyn(IEnumerable<QualificacaoSocio> model)
    {
        using (var context = new Context())
        {
            await context.AddRangeAsync(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(QualificacaoSocio model)
        => await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.QualificacaoSocios!.RemoveRange(context.QualificacaoSocios);
                    context.SaveChanges();
                }
            });

    public async IAsyncEnumerable<QualificacaoSocio> DoListAsync(Expression<Func<QualificacaoSocio, bool>>? filter = null)
    {
        using (var context = new Context())
        {
            var _query = context.QualificacaoSocios!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            foreach (var item in await _query.ToListAsync())
                yield return item;

        }
    }
}