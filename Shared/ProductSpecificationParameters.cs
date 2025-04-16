using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationParameters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
        private int pageIndex = 1;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }


        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }


    }
}
