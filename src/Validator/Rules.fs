namespace Validator

open System
open Models
open Infrastructure

module internal  Rules =
        
        let haveAName item = 
            given item.Data.Name 
            |> check isNotEmptyString 
            |> thenIfNotAddError item "Must have a name" 

        let wasBornAtLeast18YearsAgo item =
            let occuredAtLeast18YearsAgo = occuredAtLeastYearsAgo 18
            given item.Data.BirthDate
            |> check occuredAtLeast18YearsAgo
            |> thenIfNotAddError item "Too young"

        let hasSalaryInReasonableRange item =
            let isGreaterThanMinimalSalary = isGreaterThan 1000.0M<Pounds>
            let isLowerThanCEOSalary = isLowerThan 100000.0M<Pounds>
            given item.Data.Salary
            |> check isGreaterThanMinimalSalary
            |> check isLowerThanCEOSalary
            |> thenIfNotAddWarning item "Salary value"

        let wasHiredAfterWasBorn item =
            given (item.Data.BirthDate, item.Data.HireDate)
            |> check occurredBefore
            |> thenIfNotAddError item "Hire vs Birth"
