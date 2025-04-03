using System; 
using System.Collections.Generic; 
namespace eShopLegacyMVC.ViewModel 
{ 
    public class PaginatedItemsViewModel<TEntity> where TEntity : class 
    { 
        public int ActualPage { get; set; } 
        public int ItemsPerPage { get; set; } 
        public long TotalItems { get; set; } 

        public int TotalPages {get;set;}
        //public int TotalPages => (int)Math.Ceiling((decimal)Count / PageSize); 
        public IEnumerable<TEntity> Data { get; set; } 

         public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data) 
        { 
            ActualPage = pageIndex; 
            ItemsPerPage = pageSize; 
            TotalItems = count; 
            TotalPages = (int)Math.Ceiling(((decimal)count / pageSize)); 
            Data = data; 
        } 
    } 
}