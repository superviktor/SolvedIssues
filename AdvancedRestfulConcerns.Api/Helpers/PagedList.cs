using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedRestfulConcerns.Api.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        private PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            AddRange(items);
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            return new PagedList<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}
