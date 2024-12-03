using Newtonsoft.Json;

namespace OGCP.Curriculum.API.DAL.Queries.utils
{
    public class QueryParameters
    {
        private const int maxPageSize = 20;
        private int pageSize = 10;

        public string FilterBy { get; set; }
        public string SearchBy { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string OrderBy { get; set; }
        public bool Desc { get; set; }

        public string[] SelectFields 
        { 
            get => string.IsNullOrWhiteSpace(this.Fields)
                ?
                Array.Empty<string>() 
                : this.Fields.Split(',').Select(f => f.Trim()).ToArray();
        }
        public string Fields { get; set; }
    }
}
