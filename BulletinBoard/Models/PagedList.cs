namespace BulletinBoard.Models
{
    public class PagedList<T>
    {
        public PagedList(IList<T> items, int pageNumber, double totalPagesCount)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalPagesCount = (int)totalPagesCount;
        }
    
        public int PageNumber { get; set; }
        public IList<T> Items { get; set; }
        public int TotalPagesCount { get; set; }
    }
}
