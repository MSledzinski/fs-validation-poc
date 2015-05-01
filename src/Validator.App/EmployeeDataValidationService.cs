namespace Validator.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    public class EmployeeDataValidationService : IValidatorContract
    {
        public EmployeeDataValidationService()
        {
            Mapper.CreateMap<IDictionary<string, string>, Models.Employee>()
                .ForMember(m => m.HireDate, c => c.MapFrom(d => d["HireDate"]))
                .ForMember(m => m.BirthDate, c => c.MapFrom(d => d["BirthDate"]))
                .ForMember(m => m.Salary, c => c.MapFrom(d => d["Salary"]))
                .ForMember(m => m.Name, c => c.MapFrom(d => d["Name"]));
        }

        public IEnumerable<string> Validate(IEnumerable<IDictionary<string, string>> rawDataItems)
        {
            return rawDataItems
                .Select(Mapper.Map<IDictionary<string, string>, Models.Employee>)
                .Aggregate(
                    new List<Models.Violation>(),
                    (acc, emp) =>
                        {
                            var partialResults = EmployeeValidatorService.ValidateEmployeeData(emp);
                            acc.AddRange(partialResults);

                            return acc;
                        })
                 .Select(v => string.Format("{0}: {1}", StringifySeverity(v.Severity), v.Message));
        }

        private string StringifySeverity(Models.SeverityLevels severity)
        {
            return severity.IsHigh ? "High" : "Low ";
        }
    }
}