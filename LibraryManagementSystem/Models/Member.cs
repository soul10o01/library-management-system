using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime MembershipDate { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        [StringLength(50)]
        public string MembershipType { get; set; }

        // Navigation property for loans
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual ICollection<Loan>? BookLoans { get; set; } = new List<Loan>();
    }
} 