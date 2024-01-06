using WebBTL1.Models;
using WebBTL1.Pagination;

namespace WebBTL1.Services.Interface
{
    public interface IDistrictService
    {
        void AddDistrict(District district);

        void UpdateDistrict(District district);

        void DeleteDistrict(int id);

        PaginatedList<District> PaginatedDistrict(int? pageNumber);
    }
}
