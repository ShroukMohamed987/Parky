using ParkyWeb.Models;
using ParkyWeb.Reposatories.IReposatory;

namespace ParkyWeb.Reposatories.Reposatory
{
    public class TrailRepo : Reposatory<Trail> , ITrailRepo
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TrailRepo(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
            
        }
    }
}
