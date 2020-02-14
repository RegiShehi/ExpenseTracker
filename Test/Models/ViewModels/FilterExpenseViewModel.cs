using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ExpenseTracker.Models.ViewModels
{
    public class FilterExpenseViewModel
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ClientList { get; set; }
    }
}
