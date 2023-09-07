using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Repository.Extensions;

public static class EfCoreExtension
{
    public static ConfiguredTaskAwaitable<TSource> SingleAsyncWait<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return source.SingleAsync(predicate, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<TSource> SingleAsyncWait<TSource>(
        this IQueryable<TSource> source,
        CancellationToken cancellationToken = default)
    {
        return source.SingleAsync(cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<int> CountAsyncWait<TSource>(
        this IQueryable<TSource> source,
        CancellationToken cancellationToken = default)
    {
        return source.CountAsync(cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<TSource> FirstAsyncWait<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return source.FirstAsync(predicate, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<TSource> FirstAsyncWait<TSource>(
        this IQueryable<TSource> source,
        CancellationToken cancellationToken = default)
    {
        return source.FirstAsync(cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<bool> AnyAsyncWait<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return source.AnyAsync(predicate, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<List<TSource>> ToListAsyncWait<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
    {
        return source.ToListAsync(cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<TSource?> FirstOrDefaultAsyncWait<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return source.FirstOrDefaultAsync(predicate, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<TSource?> FirstOrDefaultAsyncWait<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
    {
        return source.FirstOrDefaultAsync(cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);
    }
}