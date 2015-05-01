namespace Validator

open System
open Models
open Infrastructure
open Rules

module public EmployeeValidatorService =
    
    let ValidateEmployeeData employee =
            employee 
            |> needsTo haveAName 
            |> andAlso hasSalaryInReasonableRange
            |> andAlso wasBornAtLeast18YearsAgo
            |> andAlso wasHiredAfterWasBorn
            |> returnValidationResult
