namespace GloboDelivery.Application.Models
{
    public class PaginationModel
    {
        private int _pageNumber = 1;
        public int PageNumber 
        { 
            get => _pageNumber; 
            set => _pageNumber = value <= 0 ? 1 : value; 
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 10 || value <= 0) ? 10 : value;
        }
    }
}
