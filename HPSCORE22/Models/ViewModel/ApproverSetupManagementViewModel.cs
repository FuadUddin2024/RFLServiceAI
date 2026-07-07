namespace CSWMS.Models.ViewModel
{
    public class ApproverSetupManagementViewModel
    {
        // Sales Return Permission Setup
        public UserWiseSRPermission UserWiseSRPermission { get; set; } = new UserWiseSRPermission();
        List<int> selectedCls { get; set; } = new List<int>();
        public List<ItemModel> SalesReturnItemList { get; set; } = new List<ItemModel>();
        public List<UserManagementModel> UserList { get; set; } = new List<UserManagementModel>();

        // Sales Return Process Setup
        // Panel Permission Setup
        public List<ZoneModel> ZoneModel { get; set; } = new List<ZoneModel>();
        public panelPermissionSetupModel panelPermissionSetupModel { get; set; } = new panelPermissionSetupModel();

        // Damage Return Permission Setup
        public DamageReturnPermissionUserWise DamageReturnUserWisePermission { get; set; } = new DamageReturnPermissionUserWise();
    }
}
