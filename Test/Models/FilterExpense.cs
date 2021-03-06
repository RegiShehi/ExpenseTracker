﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class FilterExpense
    {
        public int CategoryId { get; set; }

        public int ClientId { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}
