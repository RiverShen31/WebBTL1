using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;

namespace WebBTL1.Services
{
    public class CommuneService : ICommuneService
    {
        private readonly ICommuneRepo _communeRepo;

        public CommuneService(ICommuneRepo communeRepo)
        {
            _communeRepo = communeRepo;
        }

        public void AddCommune(Commune commune)
        {
            _communeRepo.Add(commune);
        }

        public void UpdateCommune(Commune commune)
        {
            _communeRepo.Update(commune);
        }

        public void DeleteCommune(int id)
        {
            var district = _communeRepo.Find(id);
            _communeRepo.Remove(district);
        }

        public PaginatedList<Commune> PaginatedCommune(int? pageNumber)
        {
            return PaginatedList<Commune>.Create(_communeRepo.GetCommuneListByPageNumber(Validate.ValidatePageNumber(ref pageNumber)),
                                                _communeRepo.GetCommuneCount(),
                                                Validate.ValidatePageNumber(ref pageNumber), 
                                                Constant.Constant.PageSize);
		}
    }
}
