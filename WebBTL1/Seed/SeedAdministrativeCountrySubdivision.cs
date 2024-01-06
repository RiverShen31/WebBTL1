using WebBTL1.Models;
using WebBTL1.Utils;

namespace WebBTL1.Seed
{
    public class SeedAdministrativeCountrySubdivision
    {
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }
        public List<Commune> Communes { get; set; }

        public SeedAdministrativeCountrySubdivision()
        {
            var fileNameProvince = "Province.xls";
            string pathProvince = Path.Combine(Environment.CurrentDirectory, @"ExcelFile\" ,fileNameProvince);
            Provinces = ExcelFileManipulation.GetProvinces(pathProvince);

            var fileNameDistrict = "District.xls";
            var pathDistrict = Path.Combine(Environment.CurrentDirectory, @"ExcelFile\", fileNameDistrict);
            Districts  = ExcelFileManipulation.GetDistricts(pathDistrict);

            var fileNameCommune = "Commune.xls";
            var pathCommune = Path.Combine(Environment.CurrentDirectory, @"ExcelFile\", fileNameCommune);
            Communes = ExcelFileManipulation.GetCommunes(pathCommune);
        }
    }
}
