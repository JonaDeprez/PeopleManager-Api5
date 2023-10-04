using PeopleManager.Model;

namespace PeopleManager.Ui.Mvc.ApiServices
{
    public class VehicleApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VehicleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Vehicle>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("VehiclesManagerApi");
            var route = "/api/vehicles";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var vehicles = await httpResponse.Content.ReadFromJsonAsync<IList<Vehicle>>();

            return vehicles ?? new List<Vehicle>();
        }

        public async Task<Vehicle?> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VehiclesManagerApi");
            var route = $"/api/vehicles/{id}";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<Vehicle>();
        }

        public async Task<Vehicle?> Create(Vehicle vehicle)
        {
            var httpClient = _httpClientFactory.CreateClient("VehiclesManagerApi");
            var route = "/api/vehicles";
            var httpResponse = await httpClient.PostAsJsonAsync(route, vehicle);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<Vehicle>();
        }

        public async Task<Vehicle?> Update(int id, Vehicle vehicle)
        {
            var httpClient = _httpClientFactory.CreateClient("VehiclesManagerApi");
            var route = $"/api/vehicles/{id}";
            var httpResponse = await httpClient.PutAsJsonAsync(route, vehicle);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<Vehicle>();
        }

        public async Task Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VehiclesManagerApi");
            var route = $"/api/vehicles/{id}";
            var httpResponse = await httpClient.DeleteAsync(route);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
