using Kalorientagebuch.Models;

namespace Kalorientagebuch.Interfaces
{
    public interface IUserService
    {
        public int CalculateTotalDailyEnergyExpenditure(User user);
    }
}