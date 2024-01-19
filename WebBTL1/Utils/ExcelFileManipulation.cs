using ClosedXML.Excel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Format;
using Org.BouncyCastle.Tls;
using System.Data;
using System.Text;
using System.Web.Mvc;
using WebBTL1.Constant;
using WebBTL1.Models;
using WebBTL1.Services.Validation;
using WebBTL1.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebBTL1.Utils
{
    public class ExcelFileManipulation
    {
        public static List<Province> GetProvinces(string fileName)
        {
            List<Province> provinces = new List<Province>();
            using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var workBook = new HSSFWorkbook(fs);
            var sheet = workBook.GetSheetAt(0);
            for (int i=1; i<=sheet.LastRowNum-1; i++)
            {
                var row = sheet.GetRow(i);
                Province province = new();
                if (row != null)
                {
                    for (int j=0; j<row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (j)
                            {
                                case 0:
                                    province.Id = int.Parse(cell.StringCellValue);
                                    break;
                                case 1:
                                    province.Name = cell.StringCellValue;
                                    break;
                                case 2:
                                    province.Level = cell.StringCellValue;
                                    break;
                            }
                        }
                    }
                }
                provinces.Add(province);
            }
            return provinces;
        }

        public static List<District> GetDistricts(string filename)
        {
            List<District> districts = new();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(fs);
            var sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum - 1; i++)
            {
                var row = sheet.GetRow(i);
                District district = new();
                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (j)
                            {
                                case 0:
                                    district.Id = int.Parse(cell.StringCellValue);
                                    break;
                                case 1:
                                    district.Name = cell.StringCellValue;
                                    break;
                                case 2:
                                    district.Level = cell.StringCellValue;
                                    break;
                                case 3:
                                    district.ProvinceId = int.Parse(cell.StringCellValue);
                                    break;
                            }
                        }
                    }
                }
                districts.Add(district);
            }
            return districts;
        }

        public static List<Commune> GetCommunes(string filename)
        {
            List<Commune> communes = new();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(fs);
            var sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum - 1; i++)
            {
                var row = sheet.GetRow(i);
                Commune commune = new();

                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (j)
                            {
                                case 0:
                                    commune.Id = int.Parse(cell.StringCellValue);
                                    break;
                                case 1:
                                    commune.Name = cell.StringCellValue;
                                    break;
                                case 2:
                                    commune.Level = cell.StringCellValue;
                                    break;
                                case 3:
                                    commune.DistrictId = int.Parse(cell.StringCellValue);
                                    break;
                            }
                        }
                    }
                }
                communes.Add(commune);
            }
            return communes;
        }

        /*public static FileResult GenerateExcel(string fileName, IEnumerable<EmployeeViewModel> employees)
        {
            DataTable dataTable = new DataTable("Employees");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Dob"),
                new DataColumn("Age"),
                new DataColumn("Ethnic"),
                new DataColumn("Job"),
                new DataColumn("IdentityNumber"),
                new DataColumn("PhoneNumber"),
                new DataColumn("Province"),
                new DataColumn("District"),
                new DataColumn("Commune"),
                new DataColumn("Description")
            });

            foreach (var person in employees)
            {
                dataTable.Rows.Add(person.Id, person.Name, person.Dob, person.Age, person.Ethnic,
                    person.Job, person.IdentityNumber, person.PhoneNumber, person.Province,
                    person.District, person.Commune, person.Description);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }*/

            public static Response ImportEmployee(XLWorkbook excelFile)
        {
            var employees = new List<Employee>();
            try
            {
                var sheet = excelFile.Worksheet(1);
                for (var i=1; i<= sheet.LastRowUsed()!.RowNumber(); i++)
                {
                    var row = sheet.Row(i);
                    Employee employee = new();
                    StringBuilder errorMessage = new();

                    GetEmployee(sheet, row, errorMessage, employee);

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
               Employee employee)
        {
            for (var column = 1; column <= row.CellCount(); column++)
            {
                switch (column)
                {
                    case 1:
                        GetName(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 2:
                        GetDateOfBirth(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 3:
                        GetAge(sheet, row.RowNumber(), column, errorMessage, employee); break;
                    case 4:
                        GetEthnic(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 5:
                        GetJob(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 6:
                        GetIdentityNumber(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 7:
                        GetPhoneNumber(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 8:
                        GetProvince(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 9:
                        GetDistrict(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;
                    case 10:
                        GetCommune(sheet, row.RowNumber(), column, errorMessage, employee);
                        break;

                }
            }
        }

        private static void GetName(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
            Employee employee)
        {
            if (!Validate.ValidateName(sheet.Cell(row, column).Value.ToString())) 
            {
                errorMessage.Append($"Name must contain only letter. Error in row {row}, column {column}.\n");
            } else
            {
                employee.Name = sheet.Cell(row, column).Value.ToString();
            }
        }

        private static void GetDateOfBirth(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
        Employee employee)
        {
            var isValid = true;
            var dateString = sheet.Cell(row, column).Value.ToString();
            var formats = new string[] { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            var isCorrectFormat = DateTime.TryParseExact(dateString,
                formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime date);
            if (!isCorrectFormat)
            {
                errorMessage.Append($"Birthday is not in d/M/yyyy format in row {row} column {column}.\n");
                isValid = false;
            }

            if (!Validate.ValidateDob(date))
            {
                errorMessage.Append(
                    $"Birthday must in the past and after 1/1/{Constant.Constant.MinYear}. Error in row {row}. column {column}.\n");
                isValid = false;
            }

            if (isValid)
            {
                employee.Dob = date;
            }
        }

        private static void GetAge(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
            Employee employee)
        {
            var ageString = sheet.Cell(row, column).Value.ToString();

            if (!int.TryParse(ageString, out int age))
            {
                errorMessage.Append($"Age is not a valid number in row {row}, column {column}.\n");
                return;
            }

            employee.Age = age;
        }

        private static void GetEthnic(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
            Employee employee)
        {
            employee.Ethnic = sheet.Cell(row, column).Value.ToString();
        }

        private static void GetJob(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
            Employee employee)
        {
            employee.Job = sheet.Cell(row, column).Value.ToString();
        }

        private static void GetIdentityNumber(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
            Employee employee)
        {
            var identityNumber = sheet.Cell(row, column).Value.ToString();
            if (!string.IsNullOrEmpty(identityNumber) && identityNumber.Length != Constant.Constant.IdentityNumberLength)
            {
                errorMessage.Append(
                $"Citizen Identity Card Number must contains {Constant.Constant.IdentityNumberLength} digits. Error in row {row}, column {column}.\n");
            } else
            {
                employee.IdentityNumber = identityNumber;
            }
        }

        private static void GetPhoneNumber(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
            Employee employee)
        {
            var phoneNumber = sheet.Cell(row, column).Value.ToString();

            if (!string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length != Constant.Constant.PhoneNumberLength
                    && !Validate.IsPhoneNumber(phoneNumber))
            {
                errorMessage.Append(
                $"Phone Number must contains {Constant.Constant.PhoneNumberLength} digits. Error in row {row}, column {column}.\n");
            } else
            {
                employee.PhoneNumber = phoneNumber;
            }
        }

        private static void GetProvince(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage, Employee employee)
        {
            string provinceValue = sheet.Cell(row, column).Value.ToString();
            if (int.TryParse(provinceValue, out int province))
            {
                employee.Province = province;
            }
            else
            {
                errorMessage.AppendLine($"Invalid value for Province at row {row}, column {column}");
            }
        }

        private static void GetDistrict(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage, Employee employee)
        {
            string districtValue = sheet.Cell(row, column).Value.ToString();
            if (int.TryParse(districtValue, out int district))
            {
                employee.District = district;
            }
            else
            {
                errorMessage.AppendLine($"Invalid value for District at row {row}, column {column}");
            }
        }

        private static void GetCommune(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage, Employee employee)
        {
            string communeValue = sheet.Cell(row, column).Value.ToString();
            if (int.TryParse(communeValue, out int commune))
            {
                employee.Commune = commune;
            }
            else
            {
                errorMessage.AppendLine($"Invalid value for Commune at row {row}, column {column}");
            }
        }
    }
}
