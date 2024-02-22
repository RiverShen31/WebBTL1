using WebBTL1.Models;

namespace WebBTL1.Repository.Interface
{
    public interface IDiplomaRepo
    {
        void Add(Diploma diploma);

        void Update(Diploma diploma);

        void Remove(Diploma diploma);

        Diploma Find(int id);

        List<Diploma> GetDiplomaListByPageNumber(int pageNumber);

        int GetDiplomaCount();

        Diploma? GetDiplomaById(int diplomaId);

        List<Diploma> GetDiplomaList();
    }
}
