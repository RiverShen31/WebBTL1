using WebBTL1.Models;

namespace WebBTL1.Repository.Interface
{
    public interface ICommuneRepo
    {
        void Add(Commune commune);

        void Update(Commune commune);

        void Remove(Commune commune);

        Commune Find(int id);

        List<Commune> GetCommuneList();

        List<Commune> GetCommuneListByPageNumber(int pageNumber);

        string? GetCommuneNameByCommuneId(int communeId);

        List<Commune> GetCommuneByDistrictId(int districtId);

        int GetCommuneCount();
    }
}
