namespace Day_9;

public class Segment
{
    public int StartIndex { get; set; }
    public int Length { get; set; }
    public int FileId { get; set; }
    public bool IsEmpty => FileId == -1;
}