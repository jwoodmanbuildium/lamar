using System;
using System.Threading.Tasks;

namespace Lamar.IoC;

internal class AsyncDisposableWrapper : IDisposable, IAsyncDisposable
{
    private readonly IAsyncDisposable _inner;

    public AsyncDisposableWrapper(IAsyncDisposable inner)
    {
        _inner = inner;
    }

    public ValueTask DisposeAsync()
    {
        return _inner.DisposeAsync();
    }

    public void Dispose()
    {
        _inner.DisposeAsync().GetAwaiter().GetResult();
    }
}