﻿using System.ComponentModel.DataAnnotations;

namespace WebMarket.DAL.Entities
{
    public class Category
    {
        [Key]
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
