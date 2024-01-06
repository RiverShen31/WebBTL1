using WebBTL1.Models;

namespace WebBTL1.Repository.Interface
{
    public interface IDistrictRepo
    {
        void Add(District district);

        void Update(District district);

        void Remove(District district);

        District Find(int id);

        List<District> GetDistrictList();

        List<District> GetDistrictListByPageNumber(int pageNumber);

        string? GetDistrictNameByDistrictId(int districtId);

        List<District> GetDistrictByProvinceId(int provinceId);

        District GetDistrictById(int districtId);

        int GetDistrictCount();   
    }
}
