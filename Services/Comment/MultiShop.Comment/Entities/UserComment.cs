﻿namespace MultiShop.Comment.Entities
{
    public class UserComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsApproved { get; set; } // Onaylanan yorumlar uida gösterilecek..
    }
}
