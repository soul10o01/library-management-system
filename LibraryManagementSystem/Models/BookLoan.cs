using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class BookLoan
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; }

        public decimal? FineAmount { get; set; }
        public bool IsFineCollected { get; set; }
    }
} 