using ParkyWeb.Models;
using ParkyWeb.Reposatories.IReposatory;

namespace ParkyWeb.Reposatories.Reposatory
{
    
    public class NationalParkRepo : Reposatory<NationalPark>, INationalParkRepo
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NationalParkRepo(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }
    }
}
