using WebBTL1.Models;
using WebBTL1.Pagination;

namespace WebBTL1.Services.Interface
{
    public interface IDiplomaService
    {
        void AddDiploma(Diploma diploma);

        void UpdateDiploma(Diploma diploma);

        void DeleteDiploma(int id);

        PaginatedList<Diploma> PaginatedDiploma(int? pageNumber);
    }
}
