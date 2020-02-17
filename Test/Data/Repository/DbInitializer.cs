using ExpenseTracker.Data.IRepository;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Data.Repository
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                    _context.Database.Migrate();

                if (!_context.Users.Any())
                {
                    _userManager.CreateAsync(new IdentityUser
                    {
                        UserName = "shehi.regi@gmail.com",
                        NormalizedUserName = "shehi.regi@gmail.com".ToUpper(),
                        Email = "shehi.regi@gmail.com",
                        NormalizedEmail = "shehi.regi@gmail.com".ToUpper(),
                        EmailConfirmed = true
                    }, "Password01!").GetAwaiter().GetResult();

                    IdentityUser user = _context.Users.Where(u => u.Email == "shehi.regi@gmail.com").FirstOrDefault();

                    var categories = new List<string>
                    {
                        "Nafte",
                        "Telefon",
                        "Ushqime/Fast",
                        "Restorant",
                        "Veshje",
                        "Estetike",
                        "Lavanderi / Detergjente / Oral",
                        "Kafe",
                        "Qethje",
                        "Internet",
                        "Drita",
                        "Uje",
                        "Makina / Motorri",
                        "Parking",
                        "Blerje ne internet",
                        "Domain/Hosting",
                        "Facebook",
                        "Libra",
                        "Materiale per zyre",
                        "Siguracione / Taksa Zyre",
                        "Autobus",
                        "Udhetime ",
                        "Lavash makine",
                        "Deme gjate projekteve",
                        "Dhurata",
                        "Kontribute ",
                        "Celular / Kompjuter / Elektronike",
                        "Palester",
                        "Ilace / Vizita mjeksore",
                        "Punime ne shtepi",
                        "Teater / kinema",
                        "Shpenzime shkolle",
                        "Kurse / Trajnime",
                        "Gjoba makine",
                        "Kika"
                    };

                    foreach (var name in categories)
                    {
                        _context.Add(new Category
                        {
                            Name = name,
                            Description = null,
                            UserId = user.Id
                        });
                    }

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
