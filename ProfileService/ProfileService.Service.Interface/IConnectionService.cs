using ProfileService.Model;
using System;
using System.Threading.Tasks;

namespace ProfileService.Service.Interface
{
	public interface IConnectionService
	{
		Task<IConnection> Create(Guid profileId, Guid linkProfileId);
        Task Delete(Guid profileId, Guid linkProfileId);
    }
}