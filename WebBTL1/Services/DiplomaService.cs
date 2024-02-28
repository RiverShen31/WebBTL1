using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;

namespace WebBTL1.Services
{
    public class DiplomaService : IDiplomaService
    {
        private readonly IDiplomaRepo _diplomaRepo;

        public DiplomaService(IDiplomaRepo diplomaRepo)
        {
            _diplomaRepo = diplomaRepo;
        }

        public void AddDiploma(Diploma diploma)
        {
            _diplomaRepo.Add(diploma);
        }

        public void UpdateDiploma(Diploma diploma)
        {
            _diplomaRepo.Update(diploma);
        }

        public void DeleteDiploma(int id)
        {
            _diplomaRepo.Remove(_diplomaRepo.Find(id));
        }

        public PaginatedList<Diploma> PaginatedDiploma(int? pageNumber)
        {
            return PaginatedList<Diploma>.Create(_diplomaRepo.GetDiplomaListByPageNumber(Validate.ValidatePageNumber(ref pageNumber)), 
                                                _diplomaRepo.GetDiplomaCount(),
                                                Validate.ValidatePageNumber(ref pageNumber), 
                                                        Constant.Constant.PageSize);
        }
    }
}
