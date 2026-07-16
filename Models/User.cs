using System.ComponentModel.DataAnnotations.Schema;

namespace mks.Models

{
    
     [Table("custom_user")]

        public class User
        {
        internal bool EmailVerified;

        public int Id { get; set; }

            public required string Username { get; set; }

            public required string Password { get; set; }

            public string? FirstName { get; set; }

            public string? LastName { get; set; }

            public required string Email { get; set; }

            public bool IsStaff { get; set; }

            public bool IsActive { get; set; }

            public bool IsSuperuser { get; set; }

            public DateTime? LastLogin { get; set; }

            public DateTime DateJoined { get; set; }

            public required string Role { get; set; }

            public int? WorkerId { get; set; }

            public bool Disabled { get; set; }
        }
}