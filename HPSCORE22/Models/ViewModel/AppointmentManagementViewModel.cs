namespace CSWMS.Models.ViewModel
{
    public class AppointmentManagementViewModel
    {
        public List<UserManagementModel> UserList { get; set; } = new List<UserManagementModel>();
        public SalesReturnPermissionSetup SalesReturnPermissionSetup { get; set; } = new SalesReturnPermissionSetup();
        public List<ProductClassModelSalesReturn> ProductClassSellReturn { get; set; } = new List<ProductClassModelSalesReturn>();
        public List<ItemModel>SalesReturnItemList { get; set; } = new List<ItemModel>();
    }
}
