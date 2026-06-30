using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace CSWMS.Models.ViewModel
{
    public class FeedBackViewModel
    {
        public FeedabackModel FeedBackDetails { get; set; }
        public List<FeedbackListForAssign> FeedBackList { get; set; }
        public List<BrandModel> BrandList { get; set; }
        public List<StatusModel> StatusList { get; set; } = new List<StatusModel>();
        public List<ProductModel> ProductList { get; set; } = new List<ProductModel>();
        public List<ItemModel> ItemList { get; set; } = new List<ItemModel>();
        public List<WarrantyCardInfo> WarrantyCardList { get; set; } = new List<WarrantyCardInfo>();
        public List<string> SelectedSubProblemList { get; set; } = new List<string>();
        public List<TechnicianModel> SolvedBy { get; set; } = new List<TechnicianModel>();
        public List<TechnicianModel>AssistBy1 { get; set; } = new List<TechnicianModel>();
        public List<TechnicianModel> AssistBy2 { get; set; } = new List<TechnicianModel>();
        public List<ComplainType> ComplainTypes { get; set; } = new List<ComplainType>();
    }

}
