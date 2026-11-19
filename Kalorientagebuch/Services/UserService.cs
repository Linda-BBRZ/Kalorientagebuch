using Kalorientagebuch.Data;
using Kalorientagebuch.Interfaces;
using Kalorientagebuch.Models;

namespace Kalorientagebuch.Services
{
    public class UserService : IUserService
    {
        private readonly KalorientagebuchDbContext _context;

        public UserService(KalorientagebuchDbContext context)
        {
            _context = context;
        }

        private double CalculateBasalMetabolicRate(User user)
        {
            double basalMetabolicRate = 0;
            if (user.Gender == "male")
            {
                // Für Männer: BMR = 66,5 + (13,75 × Gewicht in kg) + (5,003 × Größe in cm) - (6,75 × Alter)
                basalMetabolicRate = 66.5 + (13.75 * user.Weight) + (5.003 * user.Height)
                    - (6.755 * calculateAge(user.Birthday));
            }
            else if (user.Gender == "female")
            {
                // Für Frauen: BMR = 655,1 + (9, 563 × Gewicht in kg) +(1, 850 × Körpergröße in cm) -(4, 676 × Alter)
                basalMetabolicRate = 655.1 + (9.563 * user.Weight) + (1.850 * user.Height)
                    - (4.676 * calculateAge(user.Birthday));
            }
            else
            {
                // Geschlecht unbekannt oder nicht angegeben, Standardwert verwenden
                basalMetabolicRate = 0;
            }
            return basalMetabolicRate;
        }

        //TDEE (Total Daily Energy Expenditure)
        public int CalculateTotalDailyEnergyExpenditure(User user)
        {
            double calculateBasalMetabolicRate = CalculateBasalMetabolicRate(user);
            var activityLevel = _context.ActivityLevels.FirstOrDefault(a => a.Id == user.ActivityLevelId);
            double factor = activityLevel?.Factor ?? 1.0;
            if (calculateBasalMetabolicRate > 0 && factor > 0)
            {
                //userViewmodel.calculatedTDEE = Math.Round(calculateBasalMetabolicRate * factor);
                return Convert.ToInt32(calculateBasalMetabolicRate * factor);
            }
            else
            {
                return 0;
            }
        }

        private int calculateAge(DateOnly birthday)
        {
            int age = DateTime.Today.Year - birthday.Year;
            // TODO: Check if the birthday has already occurred this year, and adjust the age accordingly
            return age;
        }
    }
}
