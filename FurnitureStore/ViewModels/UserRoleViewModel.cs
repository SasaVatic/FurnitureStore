
namespace FurnitureStore.ViewModels
{
    public class UserRoleViewModel
    {
        #region Properties
        public byte Id { get; set; }
        public string RoleType { get; set; }
        #endregion

        #region Constructors
        public UserRoleViewModel(byte id, string roleType)
        {
            Id = id;
            RoleType = roleType;
        }
        #endregion
    }
}