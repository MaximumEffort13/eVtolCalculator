namespace Application.DTO;

public sealed class BladeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Length { get; set; }
    public string Width { get; set; }
    public string Thickness { get; set; }
    public string Weight { get; set; }
    public string AngleOfAttack { get; set; }
}
