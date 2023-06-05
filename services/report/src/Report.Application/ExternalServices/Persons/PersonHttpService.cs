using Report.Application.Common;
using Report.Application.ExternalServices.Persons.Model;

namespace Report.Application.ExternalServices.Persons
{
    public class PersonHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ContactInformation>> GetAll()
        {
            HttpService httpService = new HttpService(_httpClientFactory);

            return await httpService.Get<List<ContactInformation>>("http://gateway/contactmanager/person/contacts");
        }
    }
}
