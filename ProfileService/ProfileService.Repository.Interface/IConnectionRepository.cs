using ProfileService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IConnectionRepository : IRepository<Connection>
	{
		Task<Connection> GetByProfileIdAndLinkId(Guid profileId, Guid profileLinkId);
	}
}