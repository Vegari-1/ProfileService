using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IConnectionRequestRepository : IRepository<ConnectionRequest>
	{
		Task<ConnectionRequest> GetByProfileIdAndLinkId(Guid profileId, Guid profileLinkId);
	}
}