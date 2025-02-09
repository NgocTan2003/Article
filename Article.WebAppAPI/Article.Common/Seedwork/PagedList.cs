using System.Text.Json.Serialization;

namespace Article.Common.Seedwork
{
    public class PagedList<T>
    {
        public List<T> TotalItems { get; set; } //list items
        public int PageSize { get; set; } //số item/trang
        public int CurrentPage { get; set; } //trang hiện tại

        public int TotalPages { get; set; } //tổng trang
        public int StartPage { get; set; } //trang bắt đầu
        public int EndPage { get; set; } //trang kết thúc

        public PagedList() { }

        public PagedList(List<T> items, int currentPage, int pageSize, int totalPages, int startPage, int endPage)
        {
            TotalItems = items;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public PagedList(IQueryable<T> totalItems, int page, int pageSize)
        {
            //làm tròn tổng items/10 items trên 1 trang VD:16 items/10 = tròn 2 trang
            int sumItems = totalItems.Count();
            int totalPages = (int)Math.Ceiling(sumItems / (decimal)pageSize); // 33/10 = 3.3 => 4 trang
            int currentPage = page; //page hiện tại = 1
            int startPage = currentPage - 3; //trang bắt đầu trừ 5 button / 10-5 = 5
            int endPage = currentPage + 2; //trang cuối sẽ cộng thêm 4 button 5 + 4

            if (startPage <= 0)
            {
                //nếu số trang bắt đầu nhỏ hơn or = 0 thì số trang cuối sẽ bằng 
                endPage = endPage - (startPage - 1); //6 - (-3 - 1) = 10;
                startPage = 1;
            }
            if (endPage > totalPages) //nếu số page cuối > số tổng trang 
            {
                endPage = totalPages; //số page cuối = số tổng trang
                if (endPage > 10) //nếu số page cuối > 10
                {
                    startPage = endPage - 9; //trang bắt đầu = trang cuối - 9
                }
            }

            TotalItems = totalItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
