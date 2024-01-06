using WebBTL1.Models;
using WebBTL1.Pagination;

namespace WebBTL1.Services.Interface
{
    public interface IProvinceService
    {
        void AddProvince(Province province);

        void UpdateProvince(Province province);

        void DeleteProvince(int id);

        PaginatedList<Province> PaginatedProvince(int? pageNumber);
    }
}
