﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Coree.Models
{
    public class PaginationResult<T>
    {
        public PaginationResult()
        {

        }

        public PaginationResult(int page, int totalPage, int pageSize, int itemsCount, List<T> data)
        {
            Page = page;
            TotalPages = totalPage;
            PageSize = pageSize;
            ItemsCount = itemsCount;
            Data = data;
        }

        public int Page { get; set; }
        public int TotalPages{ get; set; }
        public int PageSize{ get; set; }
        public int ItemsCount{ get; set; }
        public List<T> Data { get; set;}
    }
}
