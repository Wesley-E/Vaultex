using System.ComponentModel.DataAnnotations;

namespace Vaultex.Models;

public class Organisation
{
    public string? OrganisationName { get; set; }
    [Key]
    public string? OrganisationNumber { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? Town { get; set; }
    public string? Postcode { get; set; }
    public int Column9 { get; set; }
    
}