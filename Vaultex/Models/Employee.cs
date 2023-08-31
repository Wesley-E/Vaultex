using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Vaultex.Models;

public class Employee
{
    [Key]
    public Guid EmployeeId { get; set; }
    public string? OrganisationNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}