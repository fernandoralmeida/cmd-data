namespace migradata.Helpers;
public class DisposableList<T> : List<T>, IDisposable
{
    private bool _disposed = false;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // Liberar recursos gerenciados aqui (se houver)
        if (!_disposed)
            ReleaseItem();

        // Liberar recursos não gerenciados aqui (se houver)
        _disposed = true;
    }

    public void ReleaseItem()
    {   
        // Liberar os itens da lista para o coletor de lixo
        Clear();
    }

    ~DisposableList()
    {
        Dispose(false);
    }
}