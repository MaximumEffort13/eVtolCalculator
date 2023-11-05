using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClient.Abstractions;

namespace ApiClient.DataTransferObjects.IdentityObjects;

public class LoggedInUserModel : ILoggedInUserModel
{
    public Guid Id { get; set; }

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string StreetName { get; set; }

    public string Province { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }
}
