using WebBTL1.Models;
using WebBTL1.Pagination;

namespace WebBTL1.Services.Interface
{
    public interface ICommuneService
    {
        void AddCommune(Commune commune);

        void UpdateCommune(Commune commune);

        void DeleteCommune(int id);

        PaginatedList<Commune> PaginatedCommune(int? pageNumber);
    }
}
