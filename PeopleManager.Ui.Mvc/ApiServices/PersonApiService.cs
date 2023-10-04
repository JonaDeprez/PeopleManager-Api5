using PeopleManager.Model;

namespace PeopleManager.Ui.Mvc.ApiServices
{
    public class PersonApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Person>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/people";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var people = await httpResponse.Content.ReadFromJsonAsync<IList<Person>>();

            return people ?? new List<Person>();
        }

        public async Task<Person?> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<Person>();
        }

        public async Task<Person?> Create(Person person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/people";
            var httpResponse = await httpClient.PostAsJsonAsync(route, person);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<Person>();
        }

        public async Task<Person?> Update(int id, Person person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";
            var httpResponse = await httpClient.PutAsJsonAsync(route, person);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<Person>();
        }

        public async Task Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";
            var httpResponse = await httpClient.DeleteAsync(route);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
