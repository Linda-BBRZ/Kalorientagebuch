using Kalorientagebuch.Models;
using Kalorientagebuch.Models.Requests;
using Kalorientagebuch.Models.Responses;
using Kalorientagebuch.Viewmodels;

namespace Kalorientagebuch.Interfaces
{
    public interface IUserMapper
    {
        //public void Map(User user, UserViewmodel userViewmodel);

        public void Map(User user, UserEditRequest userEditRequest);

        public User Map(UserViewmodel userViewmodel);

        public UserDetailResponse MapUserDetailResponse(User user);

        public UserEditRequest MapUserEditRequest(User user);

        public IndexItemUserViewModel Map(User user);

        public User Map(UserCreateRequest userCreateRequest);
    }
}
