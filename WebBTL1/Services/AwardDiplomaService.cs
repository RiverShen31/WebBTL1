using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;
using WebBTL1.ViewModels;

namespace WebBTL1.Services
{
    public class AwardDiplomaService : IAwardDiplomaService
    {
        private readonly IAwardDiplomaRepo _awardDiplomaRepo;
        private readonly IEmployeesRepo _employeesRepo;
        private readonly IDiplomaRepo _diplomaRepo;
        private readonly IProvinceRepo _provinceRepo;

        public AwardDiplomaService(IAwardDiplomaRepo awardDiplomaRepo, IEmployeesRepo employeesRepo,
                                    IDiplomaRepo diplomaRepo, IProvinceRepo provinceRepo)
        {
            _awardDiplomaRepo = awardDiplomaRepo;
            _employeesRepo = employeesRepo;
            _diplomaRepo = diplomaRepo;
            _provinceRepo = provinceRepo;
        }

        public void AddAwardDiploma(AwardDiploma awardDiploma)
        {
            _awardDiplomaRepo.Add(awardDiploma);
        }

        public void UpdateAwardDiploma(AwardDiploma awardDiploma)
        {
            _awardDiplomaRepo.Update(awardDiploma);
        }

        public void DeleteAwardDiploma(int id)
        {
            _awardDiplomaRepo.Remove(_awardDiplomaRepo.Find(id));
        }

        public AwardDiplomaViewModel SetAwardDiplomaViewModel(AwardDiploma? awardDiploma)
        {
            return new AwardDiplomaViewModel(awardDiploma!.Id,
                                             awardDiploma.EmployeeId,
                                             _employeesRepo.GetEmployeeById(awardDiploma.EmployeeId)!.Name,
                                             awardDiploma.DiplomaId,
                                             _diplomaRepo.GetDiplomaById(awardDiploma.DiplomaId)!.Name,
                                             awardDiploma.DiplomaGrantingUnitId,
                                             _provinceRepo.GetProvinceNameByProvinceId(awardDiploma.DiplomaGrantingUnitId),
                                             awardDiploma.GrantingDate,
                                             awardDiploma.Duration);
        }

        public List<AwardDiplomaViewModel> SetAwardDiplomaViewModelList(List<AwardDiploma?> awardDiplomas)
        {
            return awardDiplomas.Select(SetAwardDiplomaViewModel).ToList();
        }

        public PaginatedList<AwardDiplomaViewModel> PaginatedAwardDiplomaViewModel(int? pageNumber)
        {
            return PaginatedList<AwardDiplomaViewModel>.Create(SetAwardDiplomaViewModelList(_awardDiplomaRepo.GetAwardDiplomaListByPageNumber(Validate.ValidatePageNumber(ref pageNumber))!),
                                                            _awardDiplomaRepo.GetAwardDiplomaCount(),
                                                        Validate.ValidatePageNumber(ref pageNumber), 
                                                            Constant.Constant.PageSize);
        }
    }
}
