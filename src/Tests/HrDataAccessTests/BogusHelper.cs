using Bogus;
using HrServices.Entities;

namespace HrDataAccessTests
{
    public class BogusHelper
    {
        static DateTime CreatedDateOld = new DateTime(year: 2010, month: 1, day: 1);
        static DateTime RecentDateOld = new DateTime(year: 1950, month: 1, day: 1);
        static DateTime RecentDateYoung = new DateTime(year: 2004, month: 1, day: 1);
        static DateTime RecentContractOld = new DateTime(year: 2010, month: 1, day: 1);
        static DateTime RecentContractNew = DateTime.Now.AddMonths(-1);

        static string[] SkillNames = ["Cleaning Lady", "Product Owner", "Front End Developer", "Back End Developer", "Full Stack Developer",
                                      "Application Engineer", "DevOps Engineer", "HR", "HR Payroll", "Scrum Master", "Team Assistant", "Controlling",
                                      "Calibration Engineer", "Software Architect", "Quality Engineer", "HR Process Expert", "Translator", 
                                      "Documentation", "UX Design", "Electrical Engineer", "Automotive Diagnostics Engineer", "Foreign Trade Specialist",
                                      "Design Engineer - Mobility Electronics", "Project Manager", "Sales", "Sales & Business Development Manager", 
                                      "SAP Developer", "Process Engineer", "Reliability Engineer", "Marketing Specialist", "Digital Marketing", 
                                      "Logistics Engineer", "Mechanical Simulation Engineer", "3D vehicle model designer", "Car Fleet Management Specialist",
                                      "Embedded Software Engineer", "PLC programmer", "Power BI Developer", "SAP Specialist", "Image Processing Engineer", 
                                      "Mechanical Engineer - Battery", "Mechanical Engineer - Production Line", "Mechanical Engineer - Steering", 
                                      "Mechanical Engineer - Power Train", "Driver", "Administration", "Brand Manager", "Safety", "IT Security", 
                                      "IT", "Security Engineer", "Open Source Engineer", "Legal", "Legal Process Specialist", "Hardware Engineer",
                                      "IT Business Analyst", "Software Tester", "Hardware Tester", "HR Operations Specialist", "Network Designer", "Architect (Building)",
                                      "Product Auditor", "Team Coordinator", "Product Engineer", "AI Engineer", "ADAS", "Braking", "Steering", "Sound Engineer"];

        static int[] PaidVacationsPerYearValues = [20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35];

        static string[] DocumentTypeNames = ["CV", "Health Inspection", "Fire Hazard Training", "Certificate", "Mandatory Certificate", "Training Certificate", "Contract",
                                             "Performance Review", "HR Internal Document", "Payroll", "VAT", "Booking Export", "SAP Export"];

        static string[] VacationTypes = ["Paid", "Unpaid", "Home Office", "Official Leave", "Travel", "Absence", "Sick Leave", "Time Credit", "Overtime"];

        public static Faker<Skill> GetSkillFaker()
        {
            return new Faker<Skill>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.Name, f => f.PickRandom(SkillNames));
        }

        public static Faker<Employee> GetEmployeeFaker()
        {
            return new Faker<Employee>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.BankAccountNumber, f => f.Finance.Account())
                .RuleFor(x => x.BirthDate, f => f.Date.Between(RecentDateOld, RecentDateYoung))
                .RuleFor(x => x.BirthPlace, f => f.Address.City())
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.MothersName, f => f.Name.FullName())
                .RuleFor(x => x.PaidVacationsPerYear, (f, current) => f.PickRandom(PaidVacationsPerYearValues))
                .RuleFor(x => x.PassportNumber, f => f.Random.AlphaNumeric(9))
                .RuleFor(x => x.PersonalIdentifierCardNumber, f => f.Random.AlphaNumeric(7))
                .RuleFor(x => x.PhoneNumber, f => f.Person.Phone)
                .RuleFor(x => x.Residence, f => f.Address.FullAddress())
                .RuleFor(x => x.Sex, f => f.PickRandom(new string[] { "male", "female" }))
                .RuleFor(x => x.SocialSecurityNumber, f => f.Random.AlphaNumeric(9))
                .RuleFor(x => x.TaxNumber, f => f.Random.AlphaNumeric(8));
        }

        public static Faker<Employment> GetEmploymentFaker()
        {
            return new Faker<Employment>()
                .RuleFor(x => x.ContractStart, f => f.Date.Between(RecentContractOld, RecentContractNew))
                .RuleFor(x => x.ContractEnd, (f, current) => (current.ContractStart.Year < DateTime.Now.Year - 5 ? current.ContractStart.AddYears(f.Random.Number(5)) :
                                                                                                                  f.Date.Between(current.ContractStart, DateTime.Now))
                                                                                                                  .OrNull(f, .3f))
                .RuleFor(x => x.CostCenter, f => f.Random.AlphaNumeric(5))
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.Currency, f => f.Finance.Currency().Code)
                .RuleFor(x => x.HourlyRate, f => f.Random.Number(200))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.PaidVacationsPerYear, f => f.PickRandom(PaidVacationsPerYearValues))
                .RuleFor(x => x.Salary, (f, current) => f.Random.Number(1200000))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.WeeklyWorkingHours, f => f.Random.Number(15, 40));
        }

        public static Faker<Booking> GetBookingFaker()
        {
            return new Faker<Booking>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.Hours, f => f.Random.Float(1, 12))
                .RuleFor(x => x.WorkingDay, f => f.Date.Between(RecentContractOld, DateTime.Now))
                .RuleFor(x => x.BookedOnDay, (f, current) => f.Date.Between(current.WorkingDay, DateTime.Now.AddDays(30)));
        }

        public static Faker<Candidate> GetCandidateFaker()
        {
            return new Faker<Candidate>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.BirthDate, f => f.Date.Between(RecentDateOld, RecentDateYoung))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.MothersName, f => f.Name.FullName())
                .RuleFor(x => x.PhoneNumber, f => f.Person.Phone)
                .RuleFor(x => x.Sex, f => f.PickRandom(new string[] { "male", "female" }))
                .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
                .RuleFor(x => x.AppliedOn, f => f.Date.Between(DateTime.Now.AddMonths(-24), DateTime.Now))
                .RuleFor(x => x.LastStateChange, (f, current) => f.Date.Between(current.AppliedOn, DateTime.Now))
                .RuleFor(x => x.ExpectedSalary, f => f.Random.Float(1, 1200000));
        }

        public static Faker<DocumentType> GetDocumentTypeFaker()
        {
            return new Faker<DocumentType>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.Name, f => f.PickRandom(DocumentTypeNames));
        }

        public static Faker<EmployeeDeadline> GetEmployeeDeadlineFaker()
        {
            return new Faker<EmployeeDeadline>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.Topic, f => f.PickRandom(DocumentTypeNames))
                .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                .RuleFor(x => x.DeadlineDate, f => f.Date.Between(RecentContractOld, DateTime.Now.AddMonths(18)))
                .RuleFor(x => x.CompletedOn, (f, current) => f.Date.Between(current.DeadlineDate.AddMonths(-6), DateTime.Now));
        }

        public static Faker<EmployeeDocument> GetEmployeeDocumentFaker()
        {
            return new Faker<EmployeeDocument>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.ValidFrom, f => f.Date.Between(RecentContractOld, DateTime.Now))
                .RuleFor(x => x.ValidTill, (f, current) => f.Date.Between(current.ValidFrom, DateTime.Now.AddMonths(36)).OrNull(f, .3f));
        }

        public static Faker<VacationType> GetVacationTypeFaker()
        {
            return new Faker<VacationType>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.Name, f => f.PickRandom(VacationTypes))
                .RuleFor(x => x.SubtractFromPaidVacation, f => f.Random.Bool())
                .RuleFor(x => x.SubtractFromWorkingHours, f => f.Random.Bool())
                .RuleFor(x => x.SalaryMultiplier, f => f.Random.Float(0, 2))
                .RuleFor(x => x.ApplyForWholeDay, f => f.Random.Bool());
        }

        public static Faker<Vacation> GetVacationFaker()
        {
            return new Faker<Vacation>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.StartDate, f => f.Date.Between(RecentContractOld, DateTime.Now.AddMonths(8)))
                .RuleFor(x => x.EndDate, (f, current) => f.Date.Between(current.StartDate, current.StartDate.AddDays(60)))
                .RuleFor(x => x.AbsenceHours, (f, current) => (current.StartDate - current.EndDate).Days * f.Random.Float(1, 8));
        }

        public static Faker<WorkingHour> GetWorkingHourFaker()
        {
            return new Faker<WorkingHour>()
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(CreatedDateOld, DateTime.Now))
                .RuleFor(x => x.UpdatedAt, (f, current) => current.CreatedAt.AddDays(1))
                .RuleFor(x => x.IsDeleted, f => false)
                .RuleFor(x => x.From, (f, current) => f.Date.Between(current.CreatedAt.AddMonths(-3), current.CreatedAt.AddMonths(3)))
                .RuleFor(x => x.To, (f, current) => f.Date.Between(current.From.AddHours(1), current.From.AddHours(12)))
                .RuleFor(x => x.Hours, f => f.Random.Float(1, 12))
                .RuleFor(x => x.WorkingDay, (f, current) => current.From)
                .RuleFor(x => x.BookedOnDay, (f, current) => f.Date.Between(current.WorkingDay, DateTime.Now.AddDays(30)));
        }

        public void CreateFake(out List<Skill> result, int number = 1)
        {
            var skillFaker = BogusHelper.GetSkillFaker();
            var employeeFaker = BogusHelper.GetEmployeeFaker();
            var employmentFaker = BogusHelper.GetEmploymentFaker();

            var skills = skillFaker.Generate(number);
            skills.ForEach(skill =>
            {
                CreateFake(out List<Employee> employees, 50);
                skill.Employees = employees;
            });

            result = skills;
        }

        public void CreateFake(out List<Employee> result, int number = 1)
        {
            var skillFaker = BogusHelper.GetSkillFaker();
            var employeeFaker = BogusHelper.GetEmployeeFaker();
            var employmentFaker = BogusHelper.GetEmploymentFaker();

            var employees = employeeFaker.Generate(number);
            employees.ForEach(employee =>
            {
                var employments = employmentFaker.Generate(4);
                employments.ForEach(employment =>
                {
                    employment.Employee = employee;
                });

                employee.Employments = employments;
            });
            result = employees;
        }

        public void CreateFake(out List<Booking> result, int number = 1)
        {
            var bookingFaker = BogusHelper.GetBookingFaker();

            var bookings = bookingFaker.Generate(number);

            bookings.ForEach(booking =>
            {
                CreateFake(out List<Employee> employees, 1);
                booking.Employee = employees.First();
                booking.EmployeeId = employees.First().Id;
            });

            result = bookings;
        }

        public void CreateFake(out List<Candidate> result, int number = 1)
        {
            var candidateFaker = BogusHelper.GetCandidateFaker();

            result = candidateFaker.Generate(number);
        }

        public void CreateFake(out List<DocumentType> result, int number = 1)
        {
            var documentTypeFaker = BogusHelper.GetDocumentTypeFaker();

            result = documentTypeFaker.Generate(number);
        }

        public void CreateFake(out List<EmployeeDeadline> result, int number = 1)
        {
            var employeeDeadlineFaker = BogusHelper.GetEmployeeDeadlineFaker();

            var employeeDeadlines = employeeDeadlineFaker.Generate(number);

            employeeDeadlines.ForEach(deadline =>
            {
                CreateFake(out List<Employee> employees, 1);
                deadline.Employee = employees.First();
                deadline.EmployeeId = employees.First().Id;
            });

            result = employeeDeadlines;
        }
        
        public void CreateFake(out List<EmployeeDocument> result, int number = 1)
        {
            var faker = BogusHelper.GetEmployeeDocumentFaker();

            var documents = faker.Generate(number);

            documents.ForEach(document =>
            {
                CreateFake(out List<Employee> employees, 1);
                document.Employee = employees.First();
                document.EmployeeId = document.Employee.Id;

                CreateFake(out List<DocumentType> documentTypes, 1);
                document.DocumentType = documentTypes.First();
                document.DocumentTypeId = document.DocumentType.Id;
            });

            result = documents;
        }

        public void CreateFake(out List<VacationType> result, int number = 1)
        {
            var faker = BogusHelper.GetVacationTypeFaker();

            result = faker.Generate(number);
        }

        public void CreateFake(out List<Vacation> result, int number = 1)
        {
            var faker = BogusHelper.GetVacationFaker();

            var vacations = faker.Generate(number);

            vacations.ForEach(vacation =>
            {
                CreateFake(out List<Employee> employees, 1);
                vacation.Employee = employees.First();
                vacation.EmployeeId = vacation.Employee.Id;

                CreateFake(out List<VacationType> vacationTypes, 1);
                vacation.VacationType = vacationTypes.First();
                vacation.VacationTypeId = vacation.VacationType.Id;
            });

            result = vacations;
        }

        public void CreateFake(out List<WorkingHour> result, int number = 1)
        {
            var faker = BogusHelper.GetWorkingHourFaker();

            var workingHours = faker.Generate(number);

            workingHours.ForEach(workingHour =>
            {
                CreateFake(out List<Employee> employees, 1);
                workingHour.Employee = employees.First();
                workingHour.EmployeeId = employees.First().Id;
            });

            result = workingHours;
        }
    }
}
