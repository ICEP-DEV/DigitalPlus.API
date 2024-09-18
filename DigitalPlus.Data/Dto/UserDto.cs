using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Dto
{
    internal class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }

        // Role field to determine user type ("mentor", "mentee", "administrator")
        public string Role { get; set; }

        // Additional properties specific to the role

        // Mentor-specific properties
        public int? AvailabilityId { get; set; }  // Nullable, only required for Mentors

        // Mentee and Administrator-specific properties
        public int? DepartmentId { get; set; }  // Nullable, required for Mentees and Administrators

        // Mentee-specific properties
        public string? Semester { get; set; }  // Optional, default value can be set in the controller if not provided

    }
}
