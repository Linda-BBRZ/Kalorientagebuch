using Kalorientagebuch.Models;
using Kalorientagebuch.Interfaces;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Models.Responses;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Mapper
{
    public class UserMapper : IUserMapper
    {

        public void Map(User user, UserEditRequest userEditRequest)
        {
            user.Id = userEditRequest.Id;
            user.Name = userEditRequest.Name;
            user.Birthday = userEditRequest.Birthday;
            user.Weight = userEditRequest.Weight;
            user.Height = userEditRequest.Height;
            user.Gender = userEditRequest.Gender;
            user.ActivityLevelId = userEditRequest.ActivityLevelId;
        }

        public User Map(UserViewmodel viewmodel)
        {
            return new User
            {
                Id = viewmodel.Id,
                Name = viewmodel.Name,
                Birthday = viewmodel.Birthday,
                Weight = viewmodel.Weight,
                Height = viewmodel.Height,
                Gender = viewmodel.Gender,
                ActivityLevelId = viewmodel.ActivityLevelId,
            };
        }

        public UserDetailResponse MapUserDetailResponse(User user)
        {
            return new UserDetailResponse
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday,
                Weight = user.Weight,
                Height = user.Height,
                Gender = user.Gender,
            };
        }

        public UserEditRequest MapUserEditRequest(User user)
        {
            return new UserEditRequest
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday,
                Weight = user.Weight,
                Height = user.Height,
                Gender = user.Gender,
                ActivityLevelId = user.ActivityLevelId,
            };
        }

        public IndexItemUserViewModel Map(User user)
        {
            return new IndexItemUserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Birthday = user.Birthday,
                Weight = user.Weight,
                Height = user.Height,
                Gender = user.Gender,
            };
        }

        public User Map(UserCreateRequest userCreateRequest)
        {
            User user = new User();
            user.Name = userCreateRequest.Name;
            user.Birthday = userCreateRequest.Birthday;
            user.Weight = userCreateRequest.Weight;
            user.Height = userCreateRequest.Height;
            user.Gender = userCreateRequest.Gender;
            user.ActivityLevelId = userCreateRequest.ActivityLevelId;

            return user;
        }
    }
}
