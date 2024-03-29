﻿namespace WebBTL1.Constant
{
    public abstract class Constant
    {
        public const int MaxLengthName = 100;
        public const int MinLengthName = 3;
        public const int IdentityNumberLength = 11;
        public const int PhoneNumberLength = 10;
        public const int MinYear = 1950;
        public const int PageSize = 10;
        public const int PageDefault = 1;
        public const int MaxDuration = 20;
        public const int MaxNumberOfDiplomasOfEachEmployee = 3;

        public const string FileNameEmployee = "Employee.xlsx";
        public const string FileExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
}
