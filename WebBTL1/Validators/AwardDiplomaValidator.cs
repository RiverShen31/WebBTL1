﻿using FluentValidation;
using WebBTL1.Models;
using WebBTL1.Repository.Interface;

namespace WebBTL1.Validators
{
    public class AwardDiplomaValidator : AbstractValidator<AwardDiploma>
    {
        private readonly IAwardDiplomaRepo _awardDiplomaRepo;

        public AwardDiplomaValidator(IAwardDiplomaRepo awardDiplomaRepo) 
        {
            _awardDiplomaRepo = awardDiplomaRepo;

            RuleFor(x => x.Duration).LessThanOrEqualTo(Constant.Constant.MaxDuration)
                .WithMessage("Duration must be from 1 to " + Constant.Constant.MaxDuration)
                .GreaterThanOrEqualTo(1).WithMessage("Duration must be from 1 to " + Constant.Constant.MaxDuration);
            
            RuleFor(x => x).Must(x => IsValidNumberOfDiplomaOfEachEmployee(x.EmployeeId, x.Id))
                .WithMessage($"Number of diplomas for an employee cannot exceed " +
                $"{Constant.Constant.MaxNumberOfDiplomasOfEachEmployee}");

            RuleFor(x => new { x.EmployeeId, x.DiplomaId })
                .Must((awardDiploma, _) => BeUniqueDiplomaIdForEmployee(awardDiploma))
                .WithMessage("Duplicate DiplomaId for the same EmployeeId is not allowed");
        }

        private bool IsValidNumberOfDiplomaOfEachEmployee(int employeeId, int currentRecordId)
        {
            return _awardDiplomaRepo.CountNumberOfDiplomaOfEachEmployee(employeeId, currentRecordId) <= Constant.Constant.MaxNumberOfDiplomasOfEachEmployee -1;
        }

        private bool BeUniqueDiplomaIdForEmployee(AwardDiploma awardDiploma)
        {
            var employeeId = awardDiploma.EmployeeId;
            var diplomaId = awardDiploma.DiplomaId;
            var currentRecordId = awardDiploma.Id;

            // Check if there is no other record with the same DiplomaId for the same EmployeeId
            return !_awardDiplomaRepo.HasDuplicateDiplomaIdForEmployee(employeeId, diplomaId, currentRecordId);
        }
    }
}
