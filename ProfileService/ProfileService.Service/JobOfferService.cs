using Newtonsoft.Json;
using ProfileService.Model;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;
using ProfileService.Service.Interface.Exceptions;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Service
{
	public class JobOfferService : IJobOfferService
	{

        private static readonly HttpClient _client = new HttpClient();
        private static string jobOfferServiceUrl = Environment.GetEnvironmentVariable("JOB_OFFER_SERVICE_URL");

        private readonly IProfileRepository _profileRepository;

        public JobOfferService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
            if (jobOfferServiceUrl == null)
            {
                jobOfferServiceUrl = "http://localhost:5003";
            }
        }

        public async Task ShareJobOffer(string apiKey, JobOffer jobOffer)
        {
            Profile profile = await _profileRepository.GetByApiKey(apiKey);
            jobOffer.GlobalId = profile.Id.ToString();

            var requestContent = new StringContent(JsonConvert.SerializeObject(jobOffer), Encoding.UTF8, "application/json");
            Console.WriteLine(requestContent);
            try
            {
                var response = await _client.PostAsync(jobOfferServiceUrl + "/api/joboffer", requestContent);
                var responseContentString = await response.Content.ReadAsStringAsync();
                var responseContentObject = JsonConvert.DeserializeObject<Profile>(responseContentString);
                return;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApiException("JobOfferService");
            }
        }
    }
}