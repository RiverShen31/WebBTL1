using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.ViewModels;

namespace WebBTL1.Services.Interface
{
    public interface IAwardDiplomaService
    {
        void AddAwardDiploma(AwardDiploma awardDiploma);

        void UpdateAwardDiploma(AwardDiploma awardDiploma);

        void DeleteAwardDiploma(int id);

        AwardDiplomaViewModel SetAwardDiplomaViewModel(AwardDiploma? awardDiploma);

        List<AwardDiplomaViewModel> SetAwardDiplomaViewModelList(List<AwardDiploma?> awardDiplomas);

        PaginatedList<AwardDiplomaViewModel> PaginatedAwardDiplomaViewModel(int? pageNumber);
    }
}
