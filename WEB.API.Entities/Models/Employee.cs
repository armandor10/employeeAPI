using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WEB.API.Entities.Models
{
    public class Employee: IEntity
    {
        [Key]
        [Column("EmployeeId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(60, ErrorMessage = "Firstname can't be longer than 60 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(60, ErrorMessage = "Lastname can't be longer than 60 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
