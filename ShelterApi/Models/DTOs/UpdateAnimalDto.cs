public class UpdateAnimalDto
{
    public string Name { get; set; } = string.Empty;

    public string Species { get; set; } = string.Empty;

    public int Age { get; set; }

    public double Weight { get; set; }

    public Guid ShelterId { get; set; }
}