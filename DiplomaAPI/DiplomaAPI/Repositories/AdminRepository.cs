using DiplomaAPI.Data;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace DiplomaAPI.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private DataContext _data;
        private IPasswordHasher _passwordHasher;
        private ILookupNormalizer _lookupNormalizer;
        public AdminRepository(DataContext data, IPasswordHasher passwordHasher, ILookupNormalizer lookupNormalizer)
        {
            _data = data;
            _passwordHasher = passwordHasher;
            _lookupNormalizer = lookupNormalizer;
        }

        public void seedDb()
        {
            /*_data.Departments.Add( new Department{ DepartmentId = 1, Name = "Хірургічне відділення"});
            _data.Departments.Add(new Department { DepartmentId = 2, Name = "Приймальне відділення" });
            _data.Departments.Add(new Department { DepartmentId = 3, Name = "Реанімаційне відділення" });

            _data.Institutions.Add(new Institution { InstitutionId = 12345678, Name = "КНП \"Заболотівська лікарня\"", Adress = "вул. Заводська 78А" });
            _data.Institutions.Add(new Institution { InstitutionId = 87654321, Name = "КНП \"Івано-Франківська лікарня\"", Adress = "вул. Вернадського 12В" });
            _data.Institutions.Add(new Institution { InstitutionId = 21032002, Name = "КНП \"Львівська лікарня\"", Adress = "вул. Степана Бандери 13А" });

            _data.Positions.Add(new Position { PositionId = 1, PositionName = "Хірург" });
            _data.Positions.Add(new Position { PositionId = 2, PositionName = "Анастезіолог" });
            _data.Positions.Add(new Position { PositionId = 3, PositionName = "Рентгенолог" });

            _data.institutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 12345678, DepartmentId = 1 });
            _data.institutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 12345678, DepartmentId = 2 });
            _data.institutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 12345678, DepartmentId = 3 });
            _data.institutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 87654321, DepartmentId = 2 });
            _data.institutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 87654321, DepartmentId = 3 });
            _data.institutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 21032002, DepartmentId = 3 });

            _data.Employees.Add(
                new Employee
                {
                    Id = 1,
                    Institution = _data.Institutions.Find(12345678),
                    Department = _data.Departments.Find(1),
                    Email = "slobodian.vit2103@gmail.com",
                    UserName = "slobodian.vit2103@gmail.com",
                    Surname = "Слободян",
                    Name = "Віталій",
                    Patronymic = "Русланович",
                    PhoneNumber = "+380970906860",
                    DateOfBirth = new DateTime(2002,03,21),
                    Position = _data.Positions.Find(1),
                    Gender = "Чоловік",
                    EmailConfirmed = true,
                    NormalizedUserName = _lookupNormalizer.NormalizeName("slobodian.vit2103@gmail.com"),
                    NormalizedEmail = _lookupNormalizer.NormalizeEmail("slobodian.vit2103@gmail.com"),
                    PasswordHash = _passwordHasher.HashPassword("Vitalii2103@m"),
                }
                );
            _data.Employees.Add(
                new Employee
                {
                    Id = 2,
                    Institution = _data.Institutions.Find(12345678),
                    Department = _data.Departments.Find(2),
                    Email = "oleksandr.zavozin1982@gmail.com",
                    UserName = "oleksandr.zavozin1982@gmail.com",
                    Surname = "Завозін",
                    Name = "Олександр",
                    Patronymic = "Сергійович",
                    PhoneNumber = "+380970702341",
                    DateOfBirth = new DateTime(1982, 05, 27),
                    Position = _data.Positions.Find(2),
                    Gender = "Чоловік",
                    EmailConfirmed = true,
                    NormalizedUserName = _lookupNormalizer.NormalizeName("oleksandr.zavozin1982@gmail.com"),
                    NormalizedEmail = _lookupNormalizer.NormalizeEmail("oleksandr.zavozin1982@gmail.com"),
                    PasswordHash = _passwordHasher.HashPassword("Sasha0987@S"),
                }
                );
            _data.Employees.Add(
                new Employee
                {
                    Id = 3,
                    Institution = _data.Institutions.Find(12345678),
                    Department = _data.Departments.Find(3),
                    Email = "victoria.kotlarenko100@gmail.com",
                    UserName = "victoria.kotlarenko100@gmail.com",
                    Surname = "Котляренко",
                    Name = "Вікторія",
                    Patronymic = "Артемівна",
                    PhoneNumber = "+380730907832",
                    DateOfBirth = new DateTime(1994, 01, 12),
                    Position = _data.Positions.Find(3),
                    Gender = "Жінка",
                    EmailConfirmed = true,
                    NormalizedUserName = _lookupNormalizer.NormalizeName("victoria.kotlarenko100@gmail.com"),
                    NormalizedEmail = _lookupNormalizer.NormalizeEmail("victoria.kotlarenko100@gmail.com"),
                    PasswordHash = _passwordHasher.HashPassword("VictoriaKot210@"),
                }
                );

            _data.Patients.Add(
                    new Patient
                    {
                        Surname = "Слободян",
                        Name = "Ганна",
                        Patronymic = "Степанівна",
                        DateOfBirth = new DateTime(1994, 01, 12),
                        PhoneNumber = "+380686013255",
                        City = "Тернопіль"
                    }
                );
            _data.Patients.Add(
                new Patient
                    {
                        Surname = "Богайчук",
                        Name = "Сергій",
                        Patronymic = "Михайлович",
                        DateOfBirth = new DateTime(1997, 05, 12),
                        PhoneNumber = "+380354830549",
                        City = "Тернопіль"
                    }
                );
            _data.Patients.Add(
                    new Patient
                    {
                        Surname = "Романюк",
                        Name = "Петро",
                        Patronymic = "Дмитрович",
                        DateOfBirth = new DateTime(1960, 01, 04),
                        PhoneNumber = "+380671943703",
                        City = "Дрогобич"
                    }
                    );
            _data.Patients.Add(
                    new Patient
                    {
                        Surname = "Голован",
                        Name = "Галина",
                        Patronymic = "Анатоліївна",
                        DateOfBirth = new DateTime(1995, 10, 06),
                        PhoneNumber = "+380988376146",
                        City = "Львів"
                    }
                    );
            _data.Patients.Add(
                    new Patient
                    {
                        Surname = "Краховецька",
                        Name = "Ніла",
                        Patronymic = "Олександрівна",
                        DateOfBirth = new DateTime(1969, 08, 27),
                        PhoneNumber = " +380969586625",
                        City = "Івано-Франківськ"
                    }
                    );*/

            /* _data.Services.Add(new Service { ServiceId = "90762-00", ServiceName = "План лікування за допомогою фармакотерапії, початковий курс" });
             _data.Services.Add(new Service { ServiceId = "96197-19", ServiceName = "Внутрішньом'язове введення фармакологічного засобу, інший та неуточнений фармакологічний засіб" });
             _data.Services.Add(new Service { ServiceId = "96199-19", ServiceName = "Внутрішньовенне введення фармакологічного засобу, інший та неуточнений фармакологічний засіб" });
             _data.Services.Add(new Service { ServiceId = "96072-00", ServiceName = "Консультування або навчання щодо призначених/самостійно обраних лікарських засобів" });
             _data.Services.Add(new Service { ServiceId = "96202-19", ServiceName = "Ентеральне введення фармакологічного засобу, інший та неуточнений фармакологічний засіб" });
             _data.Services.Add(new Service { ServiceId = "97083-00", ServiceName = "Томографічний аналіз" });
             _data.Services.Add(new Service { ServiceId = "96021-00", ServiceName = "Оцінка догляду за собою/самообслуговування" });
             _data.Services.Add(new Service { ServiceId = "96238-00", ServiceName = "Оцінка когнітивної сфери та/або поведінки" });
             _data.Services.Add(new Service { ServiceId = "96019-00", ServiceName = "Оцінка біомеханічних функцій" });*/

            _data.Referrals.Add(
                     new Referral
                     {
                         ReferralId = "3399-6655-3752-4344",
                         Doctor = _data.Employees.Find(1),
                         Status = "Активне",
                         ProcessStatus = "Погашений (від 12.10.2022)",
                         Priority = "Плановий",
                         Category = _data.ReferralCategories.Find(1),
                         Service = _data.Services.Find("90762-00"),
                         Patient = _data.Patients.Find(1),
                         Validity = DateTime.Now.AddYears(1),
                     }
                );

            _data.Referrals.Add(
                     new Referral
                     {
                         ReferralId = "8544-7178-6738-8601",
                         Doctor = _data.Employees.Find(1),
                         Status = "Активне",
                         ProcessStatus = "Погашений (від 13.10.2022)",
                         Priority = "Плановий",
                         Category = _data.ReferralCategories.Find(1),
                         Service = _data.Services.Find("96072-00"),
                         Patient = _data.Patients.Find(1),
                         Validity = DateTime.Now.AddYears(1),
                     }
                );

            _data.Referrals.Add(
                     new Referral
                     {
                         ReferralId = "4615-6809-1327-8454",
                         Doctor = _data.Employees.Find(1),
                         Status = "Активне",
                         ProcessStatus = "Погашений (від 14.10.2022)",
                         Priority = "Плановий",
                         Category = _data.ReferralCategories.Find(1),
                         Service = _data.Services.Find("97083-00"),
                         Patient = _data.Patients.Find(1),
                         Validity = DateTime.Now.AddYears(1),
                     }
                );

            /*_data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Консультація" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Лікувально-діагностична процедура"});
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Діагностична процедура" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Госпіталізація" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Візуалізація" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Лабораторна діагностика" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Нестаціонарна паліативна допомога" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Процедура" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Нестаціонарна медична реабілітація" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Хірургічна процедура" });
            _data.ReferralCategories.Add(new ReferralCategory { CategoryName = "Переведення до іншого ЗОЗ" });*/

            _data.SaveChanges();
        }
    }
}
