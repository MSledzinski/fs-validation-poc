namespace Validator

module Models =

    open System

    // business input
    [<Measure>]
    type Pounds

    type Money = decimal<Pounds>

    [<CLIMutable>]
    type Employee = {
            Name: string
            BirthDate: DateTime
            HireDate: DateTime
            Salary: Money
         }

    // validation output
    type SeverityLevels = Low | High

    [<CLIMutable>]
    type Violation = {
            Message: string
            Severity: SeverityLevels    
        }
