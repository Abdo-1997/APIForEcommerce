using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecParms
    {
        private const int MaxPageSize = 50;
        public int PageIndx { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            //if value greaterthan page size return max page size else return value 
            set => _pageSize = (value > PageSize) ? MaxPageSize : value;
        }
      
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search 
        { 
            get => _search;
            set => value.ToLower();
         }
    }
}
