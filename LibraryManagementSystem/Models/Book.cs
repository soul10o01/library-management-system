using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(50)]
        public string Publisher { get; set; }

        public DateTime PublicationDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public string Location { get; set; }
    }
} 