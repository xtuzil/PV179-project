﻿using Infrastructure.Predicates;

namespace BL.DTOs
{
    public class FilterDto
    {
        public IPredicate Predicate { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortCriteria { get; set; }
        public bool SortAscending { get; set; }
    }
}
