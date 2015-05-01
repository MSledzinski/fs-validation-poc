namespace Validator.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("== start ==");

            try
            {
                var validator = new EmployeeDataValidationService();

                var results = validator.Validate(ProduceSomeEmployees().ToList()).ToList();

                results.ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("== end == ");
            Console.ReadLine();
        }

        private static IEnumerable<IDictionary<string, string>> ProduceSomeEmployees()
        {
            // Name rule
            yield return new Dictionary<string, string>()
                                 {
                                     { "Name", string.Empty },
                                     { "BirthDate", DateTime.Now.AddYears(-30).ToString() },
                                     { "HireDate", DateTime.Now.AddYears(-10).ToString() },
                                     { "Salary", "10000" }
                                 };

            // Hire vs Born rule
            yield return new Dictionary<string, string>()
                                 {
                                     { "Name", "Marek" },
                                     { "BirthDate", DateTime.Now.AddYears(-30).ToString() },
                                     { "HireDate", DateTime.Now.AddYears(-30).ToString() },
                                     { "Salary", "10000" }
                                 };
        }
    }
}
