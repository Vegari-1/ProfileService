using System.Threading.Tasks;

namespace ProfileService.Repository.Interface
{
	public interface IRepository<T> where T : class
	{
		Task<T> Save(T entity);
	}
}