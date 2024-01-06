using WebBTL1.Models;

namespace WebBTL1.Repository.Interface
{
    public interface IProvinceRepo
    {
        void Add(Province province);

        void Update(Province province);

        void Remove(Province province);

        Province Find(int id);

        IEnumerable<Province> GetProvinceList();

        IEnumerable<Province> GetProvinceListByPageNumber(int pageNumber);

        string? GetProvinceNameByProvinceId(int provinceId);

        Province? GetProvinceById(int provinceId);

        int GetProvinceCount();
    }
}
