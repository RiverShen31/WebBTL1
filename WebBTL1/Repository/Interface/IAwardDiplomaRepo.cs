using WebBTL1.Models;

namespace WebBTL1.Repository.Interface
{
    public interface IAwardDiplomaRepo
    {
        void Add(AwardDiploma awardDiploma);

        void Update(AwardDiploma awardDiploma);

        void Remove(AwardDiploma awardDiploma);

        AwardDiploma Find(int id);

        List<AwardDiploma?> GetAwardDiplomaListByPageNumber(int pageNumber);

        int GetAwardDiplomaCount();

        AwardDiploma? GetAwardDiplomaById(int awardDiplomaId);

        int CountNumberOfDiplomaOfEachEmployee(int employeeId, int currentRecordId);

        bool HasDuplicateDiplomaIdForEmployee(int employeeId, int diplomaId, int currentRecordId);

    }
}
