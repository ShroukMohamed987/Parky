using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.DAL.Models.Pagination
{
    public class Pagination<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public Pagination(List<T> items, int count, int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPages;

            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;

            }
        }

        public static Pagination<T> create(IQueryable<T> source, int pageIndex, int PageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
            return new Pagination<T>(items, count, pageIndex, PageSize);

        }

    }
}
