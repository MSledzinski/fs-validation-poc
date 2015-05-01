namespace Validator

open System
open FsUnit
open NUnit
open NUnit.Framework
open Models
open Infrastructure
open Rules

// Should be in separate project, but well... I'm lazy 
module public  Tests = 
        
        [<Test>]
        let ``Should have one violation for empty employee name`` () =
           // given
           let employee = { Name = String.Empty; 
                            BirthDate = DateTime.Now; 
                            HireDate = DateTime.Now; 
                            Salary = 100M<Pounds> }
           // when
           let result = employee >=> haveAName 

           //then
           result.Violations.Head.Severity |> should equal SeverityLevels.High 
           
        [<Test>]
        let ``Should have one violation for Today as birthdate`` () =
           // given
           let employee = { Name = String.Empty; 
                            BirthDate = DateTime.Now; 
                            HireDate = DateTime.Now; 
                            Salary = 100M<Pounds> }
           // when
           let result = employee >=> wasBornAtLeast18YearsAgo 

           //then
           result.Violations.Head.Severity |> should equal SeverityLevels.High
           
        [<Test>]
        let ``Should have one violation for 0 Pound salary`` () =
           // given
           let employee = { Name = String.Empty; 
                            BirthDate = DateTime.Now; 
                            HireDate = DateTime.Now; 
                            Salary = 0M<Pounds> }
           // when
           let result = employee >=> hasSalaryInReasonableRange 

           //then
           result.Violations.Head.Severity |> should equal SeverityLevels.Low
           
        [<Test>]
        let ``Should have one violation for 10000000 Pound salary`` () =
           // given
           let employee = { Name = String.Empty; 
                            BirthDate = DateTime.Now; 
                            HireDate = DateTime.Now; 
                            Salary = 10000000M<Pounds> }
           // when
           let result = employee >=> hasSalaryInReasonableRange 

           //then
           result.Violations.Head.Severity |> should equal SeverityLevels.Low

        [<Test>]
        let ``Should have one violation when hired before was born`` () =
           // given
           let employee = { Name = String.Empty; 
                            BirthDate = DateTime.Now.AddYears(-5); 
                            HireDate = DateTime.Now.AddYears(-10); 
                            Salary = 100M<Pounds> }
           // when
           let result = employee >=> wasHiredAfterWasBorn 

           //then
           result.Violations.Head.Severity |> should equal SeverityLevels.High

        [<Test>]
        let ``Should have violation for each and every Employee field`` () =
           // given
           let employee = { Name = String.Empty; 
                            BirthDate = DateTime.Now.AddYears(-5); 
                            HireDate = DateTime.Now.AddYears(-10); 
                            Salary = 1M<Pounds> }
           // when
           let result = employee 
                        |> needsTo haveAName 
                        |> andAlso hasSalaryInReasonableRange
                        |> andAlso wasBornAtLeast18YearsAgo
                        |> andAlso wasHiredAfterWasBorn

           //then
           result.Violations.Length |> should equal 4