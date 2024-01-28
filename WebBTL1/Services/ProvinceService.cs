using WebBTL1.Models;
using WebBTL1.Pagination;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;
using WebBTL1.Services.Validation;

namespace WebBTL1.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepo _provinceRepo;
        public ProvinceService(IProvinceRepo provinceRepo)
        {
            _provinceRepo = provinceRepo;
        }

        public void AddProvince(Province province)
        {
            _provinceRepo.Add(province);
        }

        public void UpdateProvince(Province province)
        {
            _provinceRepo.Update(province);
        }

        public void DeleteProvince(int id)
        {
            var province = _provinceRepo.GetProvinceById(id);
            _provinceRepo.Remove(province!);
        }

        public PaginatedList<Province> PaginatedProvince(int? pageNumber)
        {
            return PaginatedList<Province>.Create(_provinceRepo.GetProvinceListByPageNumber(Validate.ValidatePageNumber(ref pageNumber)),
														_provinceRepo.GetProvinceCount(),
											 Validate.ValidatePageNumber(ref pageNumber), Constant.Constant.PageSize);
        }
    }
}
