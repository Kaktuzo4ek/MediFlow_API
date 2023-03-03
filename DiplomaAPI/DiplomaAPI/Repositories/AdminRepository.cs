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
            _data.Departments.Add( new Department{ DepartmentId = 1, Name = "Хірургічне відділення"});
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

            _data.SaveChanges();
        }
    }
}
