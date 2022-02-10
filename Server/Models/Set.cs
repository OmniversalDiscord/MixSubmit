namespace MixSubmit.Models;

public enum SetStatus
{
    InProgress,
    Finished
}

public enum SetType
{
    Live,
    Prerecorded
}

public class Set
{
    public int SetId { get; set; }
    public string Artist { get; set; }
    public string Code { get; set; }
    public SetType SetType { get; set; }
    public SetStatus Status { get; set; }
    public string? SetUrl { get; set; }
}