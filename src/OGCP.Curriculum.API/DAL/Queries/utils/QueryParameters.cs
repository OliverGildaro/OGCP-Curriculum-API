namespace OGCP.Curriculum.API.DAL.Queries.utils
{
    public class QueryParameters
    {
        const int maxPageSize = 20;
        private int pageSize = 10;

        public string Filter { get; set; }
        public string SearchBy { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string OrderBy { get; set; }
        public bool Desc { get; set; }
    }
}
