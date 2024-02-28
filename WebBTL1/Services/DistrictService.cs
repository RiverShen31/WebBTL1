using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;

namespace WebBTL1.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepo _districtRepo;

        public DistrictService(IDistrictRepo districtRepo)
        {
            _districtRepo = districtRepo;
        }

        public void AddDistrict(District district)
        {
            _districtRepo.Add(district);
        }

        public void UpdateDistrict(District district)
        {
            _districtRepo.Update(district);
        }

        public void DeleteDistrict(int id)
        {
            _districtRepo.Remove(_districtRepo.Find(id));
        }
        public PaginatedList<District> PaginatedDistrict(int? pageNumber)
        {
            return PaginatedList<District>.Create(_districtRepo.GetDistrictListByPageNumber(Validate.ValidatePageNumber(ref pageNumber)), 
                                                _districtRepo.GetDistrictCount(),
                                                Validate.ValidatePageNumber(ref pageNumber), 
                                                Constant.Constant.PageSize);
		}
    }
}
