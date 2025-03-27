using System; 
using System.Collections.Generic; 
namespace eShopLegacyMVC.ViewModel 
{ 
    public class PaginatedItemsViewModel<T> 
    { 
        public int PageIndex { get; set; } 
        public int PageSize { get; set; } 
        public long Count { get; set; } 
        public int TotalPages { get; private set; } 
        public IEnumerable<T> Data { get; set; } 
        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<T> data) 
        { 
            PageIndex = pageIndex; 
            PageSize = pageSize; 
            Count = count; 
            TotalPages = (int)Math.Ceiling(((decimal)count / pageSize)); 
            Data = data; 
        } 
    } 
}