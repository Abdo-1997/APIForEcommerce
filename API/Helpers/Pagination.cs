using API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class Pagination<T> where T : class
    {
      
        public Pagination(int pageSize, int pageIndx, int totalItems, IReadOnlyList<T> data)
        {
            PageIndex = pageIndx;
            PageSize = pageSize;
            Count = totalItems;
            Data = data;

        }

        public int PageIndex{get;set;}
        public int PageSize{get;set;}
        public int Count{get;set;}

        public IReadOnlyList<T> Data { set; get; }

    }
}
