using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory_Activity_5
{
    internal class Program
    {
        class Employee
        {
            public string Name;
            public double HourlyRate;
            public double[] Hours;

            public Employee(string name, double rate, double[] hours)
            {
                if (hours.Length != 7)
                    throw new ArgumentException("Hours must have 7 entries (Mon–Sun).");

                foreach (var h in hours)
                {
                    if (h < 0 || h > 24)
                        throw new ArgumentException("Invalid daily hours: " + h);
                }

                Name = name;
                HourlyRate = rate;
                Hours = hours;
            }

            public double GrossPay()
            {
                double totalHours = Hours.Sum();
                double pay = 0;

                foreach (var h in Hours)
                {
                    if (h > 8)
                        pay += (8 * HourlyRate) + ((h - 8) * HourlyRate * 1.25);
                    else
                        pay += h * HourlyRate;
                }

                if (totalHours > 40)
                {
                    double overtime = totalHours - 40;
                    pay += overtime * HourlyRate * 0.5;
                }

                return pay;
            }

            public double NetPay()
            {
                double gross = GrossPay();
                double tax = 0;


                if (gross <= 500)
                    tax = gross * 0.1;
                else if (gross <= 1000)
                    tax = (500 * 0.1) + ((gross - 500) * 0.2);
                else
                    tax = (500 * 0.1) + (500 * 0.2) + ((gross - 1000) * 0.3);

                return gross - tax;
            }
        }

        static void Main()
        {
            List<Employee> employees = new List<Employee>
        {
            new Employee("Alyssa", 20, new double[] {8, 9, 8, 10, 7, 0, 0}),
            new Employee("Faye", 15, new double[] {6, 7, 8, 6, 5, 0, 0}),
            new Employee("Charly", 25, new double[] {10, 10, 10, 10, 10, 0, 0})
        };

            var sorted = employees.OrderByDescending(e => e.NetPay());

            Console.WriteLine("Weekly Payroll:\n");
            foreach (var emp in sorted)
            {
                Console.WriteLine($"Employee: {emp.Name}");
                Console.WriteLine($"  Gross Pay: {emp.GrossPay():C}");
                Console.WriteLine($"  Net Pay:   {emp.NetPay():C}\n");
            }
        }
    }
}







