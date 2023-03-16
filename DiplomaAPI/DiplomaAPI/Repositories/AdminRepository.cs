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
            /*_data.Departments.Add( new Department{ Name = "Хірургічне відділення"});
            _data.Departments.Add(new Department { Name = "Приймальне відділення" });
            _data.Departments.Add(new Department { Name = "Реанімаційне відділення" });
            _data.SaveChanges();

            _data.Institutions.Add(new Institution { InstitutionId = 12345678, Name = "КНП \"Заболотівська лікарня\"", Adress = "вул. Заводська 78А" });
            _data.Institutions.Add(new Institution { InstitutionId = 87654321, Name = "КНП \"Івано-Франківська лікарня\"", Adress = "вул. Вернадського 12В" });
            _data.Institutions.Add(new Institution { InstitutionId = 21032002, Name = "КНП \"Львівська лікарня\"", Adress = "вул. Степана Бандери 13А" });
            _data.SaveChanges();

            _data.Positions.Add(new Position { PositionName = "Хірург" });
            _data.Positions.Add(new Position { PositionName = "Анастезіолог" });
            _data.Positions.Add(new Position { PositionName = "Рентгенолог" });
            _data.SaveChanges();

            _data.InstitutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 12345678, DepartmentId = 1 });
            _data.InstitutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 12345678, DepartmentId = 2 });
            _data.InstitutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 12345678, DepartmentId = 3 });
            _data.InstitutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 87654321, DepartmentId = 2 });
            _data.InstitutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 87654321, DepartmentId = 3 });
            _data.InstitutionsAndDepartments.Add(new InstitutionAndDepartment { InstitutionId = 21032002, DepartmentId = 3 });
            _data.SaveChanges();

            _data.Doctors.Add(
                new Doctor
                {
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
            _data.Doctors.Add(
                new Doctor
                {
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
            _data.Doctors.Add(
                new Doctor
                {
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
            _data.SaveChanges();

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
                    );
            _data.SaveChanges();

            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Консультація" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Лікувально-діагностична процедура" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Діагностична процедура" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Госпіталізація" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Візуалізація" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Лабораторна діагностика" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Нестаціонарна паліативна допомога" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Процедура" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Нестаціонарна медична реабілітація" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Хірургічна процедура" });
            _data.ServiceCategories.Add(new ServiceCategory { CategoryName = "Переведення до іншого ЗОЗ" });
            _data.SaveChanges();

            _data.Services.Add(new Service { ServiceId = "90762-00", ServiceName = "План лікування за допомогою фармакотерапії, початковий курс", Category = _data.ServiceCategories.Find(8)});
             _data.Services.Add(new Service { ServiceId = "96197-19", ServiceName = "Внутрішньом'язове введення фармакологічного засобу, інший та неуточнений фармакологічний засіб", Category = _data.ServiceCategories.Find(8) });
             _data.Services.Add(new Service { ServiceId = "96199-19", ServiceName = "Внутрішньовенне введення фармакологічного засобу, інший та неуточнений фармакологічний засіб", Category = _data.ServiceCategories.Find(8) });
             _data.Services.Add(new Service { ServiceId = "96072-00", ServiceName = "Консультування або навчання щодо призначених/самостійно обраних лікарських засобів", Category = _data.ServiceCategories.Find(8) });
             _data.Services.Add(new Service { ServiceId = "96202-19", ServiceName = "Ентеральне введення фармакологічного засобу, інший та неуточнений фармакологічний засіб", Category = _data.ServiceCategories.Find(8) });
             _data.Services.Add(new Service { ServiceId = "97083-00", ServiceName = "Томографічний аналіз", Category = _data.ServiceCategories.Find(3) });
             _data.Services.Add(new Service { ServiceId = "96021-00", ServiceName = "Оцінка догляду за собою/самообслуговування", Category = _data.ServiceCategories.Find(3) });
             _data.Services.Add(new Service { ServiceId = "96238-00", ServiceName = "Оцінка когнітивної сфери та/або поведінки", Category = _data.ServiceCategories.Find(3) });
             _data.Services.Add(new Service { ServiceId = "96019-00", ServiceName = "Оцінка біомеханічних функцій", Category = _data.ServiceCategories.Find(3) });
            _data.SaveChanges();*/
        }
    }
}
