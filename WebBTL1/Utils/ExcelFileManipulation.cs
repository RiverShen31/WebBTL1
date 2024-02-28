using ClosedXML.Excel;
using NPOI.HSSF.UserModel;
using System.Text;
using NPOI.SS.UserModel;
using WebBTL1.Models;
using WebBTL1.Services.Validation;

namespace WebBTL1.Utils
{
    public abstract class ExcelFileManipulation
    {
        private const int ColumnId = 0;
        private const int ColumnName = 1;
        private const int ColumnLevel = 2;
        private static List<T> GetEntities<T>(string fileName, Func<IRow, T> createEntity)
        {
            var entities = new List<T>();
            using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(fs);
            var sheet = workbook.GetSheetAt(0);
            for (var i = 1; i <= sheet.LastRowNum - 1; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;
                var entity = createEntity(row);
                entities.Add(entity);
            }
            return entities;
        }

        private static T? GetCellValue<T>(IRow row, int columnIndex)
        {
            var cell = row.GetCell(columnIndex);
            return cell != null ? (T)Convert.ChangeType(cell.StringCellValue, typeof(T)): default(T);
        }
        
        public static List<Province> GetProvinces(string fileName)
        {
            
            return GetEntities(fileName, row => new Province
            {
                Id = int.Parse(GetCellValue<string>(row, ColumnId) ?? string.Empty),
                Name = GetCellValue<string>(row, ColumnName),
                Level = GetCellValue<string>(row, ColumnLevel)
            });
        }
        
        public static List<District> GetDistricts(string filename)
        {
            const int columnProvinceId = 3;
            return GetEntities(filename, row => new District
            {
                Id = int.Parse(GetCellValue<string>(row, ColumnId) ?? string.Empty),
                Name = GetCellValue<string>(row, ColumnName),
                Level = GetCellValue<string>(row, ColumnLevel),
                ProvinceId = int.Parse(GetCellValue<string>(row, columnProvinceId) ?? string.Empty)
            });
        }

        public static List<Commune> GetCommunes(string filename)
        {
            const int columnDistrictId = 3;
            return GetEntities(filename, row => new Commune
            {
                Id = int.Parse(GetCellValue<string>(row, ColumnId) ?? string.Empty),
                Name = GetCellValue<string>(row, ColumnName),
                Level = GetCellValue<string>(row, ColumnLevel),
                DistrictId = int.Parse(GetCellValue<string>(row, columnDistrictId) ?? string.Empty)
            });
        }

        public static Response ImportEmployee(XLWorkbook excelFile, List<Province> provinces, List<District> districts, List<Commune> communes)
        {
            var employees = new List<Employee>();
            try
            {
                var sheet = excelFile.Worksheet(1);
                const int startRow = 2; // Row Start data
                for (var i=startRow; i<= sheet.LastRowUsed()!.RowNumber(); i++)
                {
                    var row = sheet.Row(i);
                    Employee employee = new();
                    StringBuilder errorMessage = new();

                    GetEmployee(sheet, row, errorMessage, employee, provinces, districts, communes);

                    if (!string.IsNullOrEmpty(errorMessage.ToString())) 
                    {
                        return new Response(new ErrorMessage(errorMessage.ToString()));
                    }
                    employees.Add(employee);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new Response(employees);
        }

        private static void GetEmployee(IXLWorksheet sheet, IXLRow row, StringBuilder errorMessage,
               Employee employee, IEnumerable<Province> provinces, IEnumerable<District> districts, IEnumerable<Commune> communes)
        {
            employee.Name = GetName(sheet, row.RowNumber(), 1, errorMessage);
            employee.Dob = GetDateOfBirth(sheet, row.RowNumber(), 2, errorMessage);
            employee.Age = GetAge(sheet, row.RowNumber(), 3, errorMessage);
            employee.Ethnic = GetEthnic(sheet, row.RowNumber(), 4);
            employee.Job = GetJob(sheet, row.RowNumber(), 5);
            employee.IdentityNumber = GetIdentityNumber(sheet, row.RowNumber(), 6, errorMessage);
            employee.PhoneNumber = GetPhoneNumber(sheet, row.RowNumber(), 7, errorMessage);

            var province = GetProvince(sheet, row.RowNumber(), 8, errorMessage, provinces);
            var district = GetDistrict(sheet, row.RowNumber(), 9, errorMessage, districts, province);
            var commune = GetCommune(sheet, row.RowNumber(), 10, errorMessage, communes, district);

            employee.Province = province;
            employee.District = district;
            employee.Commune = commune;
        }

        private static string GetName(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage)
        {
            var nameStr = ExcelHelper.GetValue(sheet, row, column);
            if (Validate.ValidateName(nameStr)) return nameStr;
            errorMessage.Append($"Name must contain only letter. Error in row {row}, column {column}.\n");
            return string.Empty;
        }

        private static DateTime GetDateOfBirth(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage)
        {
            var dateString = ExcelHelper.GetValue(sheet, row, column);
            var formats = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            var isCorrectFormat = DateTime.TryParseExact(dateString,
                formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var date);
            if (!isCorrectFormat)
            {
                errorMessage.Append($"Birthday is not in d/M/yyyy format in row {row} column {column}.\n");
                return DateTime.Now;
            }

            if (Validate.ValidateDob(date)) return date;
            errorMessage.Append(
                $"Birthday must in the past and after 1/1/{Constant.Constant.MinYear}. Error in row {row}. column {column}.\n");
            return DateTime.Now;

        }

        private static int GetAge(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage)
        {
            var ageString = ExcelHelper.GetValue(sheet, row, column);

            if (int.TryParse(ageString, out var age)) return age;
            errorMessage.Append($"Age is not a valid number in row {row}, column {column}.\n");
            return int.MinValue;

        }

        private static string GetEthnic(IXLWorksheet sheet, int row, int column)
        {
            return ExcelHelper.GetValue(sheet, row, column);
        }

        private static string GetJob(IXLWorksheet sheet, int row, int column)
        {
            return ExcelHelper.GetValue(sheet, row, column);
        }

        private static string GetIdentityNumber(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage)
        {
            var identityNumber = ExcelHelper.GetValue(sheet, row, column);
            if (string.IsNullOrEmpty(identityNumber) || identityNumber.Length == Constant.Constant.IdentityNumberLength)
                return identityNumber;
            errorMessage.Append(
                $"Citizen Identity Card Number must contains {Constant.Constant.IdentityNumberLength} digits. Error in row {row}, column {column}.\n");
            return string.Empty;

        }

        private static string GetPhoneNumber(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage)
        {
            var phoneNumber = ExcelHelper.GetValue(sheet, row, column);

            if (string.IsNullOrEmpty(phoneNumber) 
                || phoneNumber.Length == Constant.Constant.PhoneNumberLength
                || Validate.IsPhoneNumber(phoneNumber)) 
                return phoneNumber;
            errorMessage.Append(
                $"Phone Number must contains {Constant.Constant.PhoneNumberLength} digits. Error in row {row}, column {column}.\n");
            return string.Empty;

        }

        private static int GetProvince(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage, IEnumerable<Province> provinces)
        {
            var provinceValue = ExcelHelper.GetValue(sheet, row, column);
            var matchingProvince = provinces.FirstOrDefault(p => p.Name == provinceValue);

            if (matchingProvince != null)
            {
                return matchingProvince.Id;
            }

            errorMessage.AppendLine($"Invalid value for Province at row {row}, column {column}");
            return 0; // Or any other appropriate default value
        }

        private static int GetDistrict(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage, IEnumerable<District> districts, int provinceId)
        {
            var districtValue = ExcelHelper.GetValue(sheet, row, column);
            var matchingDistrict = districts.FirstOrDefault(p => p.Name == districtValue && p.ProvinceId == provinceId);
            if (matchingDistrict != null)
            {
                return matchingDistrict.Id;
            }
            errorMessage.AppendLine($"Invalid value for District at row {row}, column {column}");
            return 0;
        }

        private static int GetCommune(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage, IEnumerable<Commune> communes, int districtId)
        {
            var communeValue = ExcelHelper.GetValue(sheet, row, column);
            var matchingCommune = communes.FirstOrDefault(p => p.Name == communeValue && p.DistrictId == districtId);
            if (matchingCommune != null)
            {
                return matchingCommune.Id;
            }
            errorMessage.AppendLine($"Invalid value for Commune at row {row}, column {column}");
            return 0;
        }
    }
}
