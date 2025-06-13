using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }

        [Required]
        public int MemberId { get; set; }
        public virtual Member? Member { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; }
        
        public string? Notes { get; set; }

        public decimal? FineAmount { get; set; }
        public bool IsFineCollected { get; set; }
    }
} 