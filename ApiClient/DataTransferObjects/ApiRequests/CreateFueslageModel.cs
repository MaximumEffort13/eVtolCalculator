using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateFueslageModel
{
    [Required]
    public double Weight_kg { get; set; }
}
