using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ExpenseTracker.Models.ViewModels
{
    public class ExpenseViewModel
    {
        public Expense Expense { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ClientList { get; set; }
    }
}
