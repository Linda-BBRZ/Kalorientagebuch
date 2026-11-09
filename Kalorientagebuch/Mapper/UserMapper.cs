using Kalorientagebuch.Models;
using Kalorientagebuch.Viewmodels;
using Microsoft.AspNetCore.Identity;

namespace Kalorientagebuch.Mapper
{
    public class UserMapper
    {

        public static UserViewmodel Map(User user)
        {
            return new UserViewmodel
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday,
                Weight = user.Weight,
                Height = user.Height,
                Gender = user.Gender,
                //ActivityLevel = user.ActivityLevel,
                ActivityLevelId = user.ActivityLevelId,
                Password = user.PasswordHash
            };
        }
        public static User Map(UserViewmodel viewmodel)
        {
            return new User
            {
                Id = viewmodel.Id,
                Name = viewmodel.Name,
                Birthday = viewmodel.Birthday,
                Weight = viewmodel.Weight,
                Height = viewmodel.Height,
                Gender = viewmodel.Gender,
                //ActivityLevel = viewmodel.ActivityLevel,
                ActivityLevelId = viewmodel.ActivityLevelId,
            };
        }

        public static void Map(User user, UserViewmodel viewmodel, IPasswordHasher<User> passwordHasher)
        {
            user.Name = viewmodel.Name;
            user.Birthday = viewmodel.Birthday;
            user.Weight = viewmodel.Weight;
            user.Height = viewmodel.Height;
            user.Gender = viewmodel.Gender;
            //user.ActivityLevel = viewmodel.ActivityLevel;
            user.ActivityLevelId = viewmodel.ActivityLevelId;
            if (!string.IsNullOrEmpty(viewmodel.Password))
            {
                user.PasswordHash = passwordHasher.HashPassword(user, viewmodel.Password);
            }
        }
    }
}
