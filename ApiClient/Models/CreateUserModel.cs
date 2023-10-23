using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Models;

public class CreateUserModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    [Required]
    [EmailAddress]
    [Compare(nameof(EmailAddress), ErrorMessage = "Emails do not match")]
    public string ConfirmEmail { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage ="Passwords do not match")]
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Building name and unit number
    /// </summary>
    [Display(Name = "Building & Unit")]
    public string Building { get; set; }

    /// <summary>
    /// The street name and number
    /// </summary>
    [Required]
    [Display(Name = "Street number and address")]
    public string StreetName { get; set; }

    [Required]
    public string Suburb { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    public string Province { get; set; }

    [Required]
    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; }
}
