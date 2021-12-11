﻿using Microsoft.EntityFrameworkCore;
using PruebaENG.Application.Common.Exceptions;
using PruebaENG.Application.Common.Wrapper;

namespace PruebaENG.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
    {
        if (source == null)
        {
            throw new CustomException("Empty");
        }

        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        pageSize = pageSize == 0 ? 10 : pageSize;
        var count = await source.AsNoTracking().CountAsync();
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
    }
}