using ProfileService.Model;
using System.Threading.Tasks;

namespace ProfileService.Service.Interface
{
	public interface IJobOfferService
	{
		Task ShareJobOffer(string apiKey, JobOffer jobOffer);
	}
}