
namespace FurnitureStore.ViewModels
{
    public class UserRoleViewModel
    {
        #region Properties
        public string Id { get; set; }
        public string RoleType { get; set; }
        #endregion

        #region Constructors
        public UserRoleViewModel(string id, string roleType)
        {
            Id = id;
            RoleType = roleType;
        }
        #endregion
    }
}