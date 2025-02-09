namespace Article.WebApp.Models
{
    public class Paginate
    {
        public int TotalItems { get; private set; } //tổng số items
        public int PageSize { get; private set; } //tổng số item/trang
        public int CurrentPage { get; private set; } //trang hiện tại

        public int TotalPages { get; private set; } //tổng trang
        public int StartPage { get; private set; } //trang bắt đầu
        public int EndPage { get; private set; } //trang kết thúc

        public Paginate(int pageSize, int currentPage, int totalPages, int startPage, int endPage)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public Paginate() { }
    }
}
