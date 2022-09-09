using ProfileService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IConnectionRequestRepository : IRepository<ConnectionRequest>
	{
		Task<ConnectionRequest> GetById(Guid id);
		Task<IEnumerable<ConnectionRequest>> GetByProfileId(Guid profileId);
		Task<ConnectionRequest> GetByProfileIdAndLinkId(Guid profileId, Guid profileLinkId);
    }
}