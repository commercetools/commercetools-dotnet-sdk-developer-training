using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Stores;


namespace Training.Services
{
    public interface IProjectService
    {
        Task<IList<string>> GetCountriesAsync();

        Task<IStorePagedQueryResponse> GetStoresAsync();
    
    }

    public class ProjectService : IProjectService
    {
        private readonly ProjectApiRoot _api;


        public ProjectService(ProjectApiRoot api)

        {
            _api = api;
        }


        public async Task<IList<string>> GetCountriesAsync()
        {
            var response = await _api
                .Get()
                .ExecuteAsync();

            return response.Countries;
        }

        public async Task<IStorePagedQueryResponse> GetStoresAsync()
        {
            var response = await _api
                .Stores()
                .Get()
                .ExecuteAsync();

            return response;
        }

    }
}
