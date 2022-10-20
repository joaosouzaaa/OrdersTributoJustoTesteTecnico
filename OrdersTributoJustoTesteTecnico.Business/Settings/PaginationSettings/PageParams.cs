namespace OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings
{
    public class PageParams
    {
        private const int MaxPageSize = 12;

        public int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value; 
        }

        public int pageNumber = 1;
        public int PageNumber
        {
            get => pageNumber; 
            set => pageNumber = (value <= 0) ? pageNumber : value;
        }
    }
}
