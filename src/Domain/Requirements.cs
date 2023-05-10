namespace PoliVagas.Core.Domain;

public class Requirements
{
    public EnglishLevel? EnglishLevel { get; set; }
    public string? OtherLanguages { get; set; }
    public string? SoftSkills { get; set; }
    public string? HardSkills { get; set; }
}

public enum EnglishLevel : ushort
{
    Beginner = 1,
    Intermediate = 2,
    Advanced = 3,
}
