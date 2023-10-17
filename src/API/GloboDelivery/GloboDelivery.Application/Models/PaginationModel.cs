namespace GloboDelivery.Application.Models
{
    public class PaginationModel
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 10) ? 10 : value;
        }
    }
}
