namespace Day_9;

public class DiskMap
{
    private List<Segment> _diskMap { get; } = new();

    public DiskMap(string compressedFileMap)
    {
        Segments = [];
        var fileIdIndex = 0;
        for (int i = 0; i < compressedFileMap.Length; i++)
        {
            if (i % 2 != 0)
            {
                Segments.Add(new Segment
                {
                    StartIndex = i,
                    Length = int.Parse(compressedFileMap[i].ToString()),
                    FileId = -1
                });
            }
            else
            {
                Segments.Add(new Segment()
                {
                    StartIndex = i,
                    Length = int.Parse(compressedFileMap[i].ToString()),
                    FileId = fileIdIndex
                });
                fileIdIndex++;
            }
        }

        CreateDiskMap();
    }

    public List<Segment> Segments { get; set; }

    public double GetChecksum()
    {
        var checksum = 0d;
        for(var i = 0; i < Segments.Count; i++)
        {
            var segment = Segments[i];
            if (segment.IsEmpty)
                continue;

            for(var j = 0; j < segment.Length; j++)
            {
                checksum += segment.FileId  * i * 1d;
            }
        }

        return checksum;
    }

    public double GetDiskMapChecksum()
    {
        Console.Clear();
        // DisplayDiskCharMap();
        var checksum = 0d;
        for (var i = 1; i < _diskMap.Count; i++)
        {
            if (_diskMap[i].IsEmpty)
                continue;

            checksum += _diskMap[i].FileId * i;
        }

        return checksum;
    }

    public void DisplayDiskMap()
    {
        foreach (var segment in Segments)
        {
            for (int i = 0; i < segment.Length; i++)
            {
                if (segment.IsEmpty)
                {
                    Console.Write('.');
                }
                else
                {
                    Console.Write(segment.FileId.ToString());
                }
            }
        }

        Console.WriteLine();
    }

    public void CreateDiskMap()
    {
        _diskMap.Clear();
        foreach (var segment in Segments)
        {
            for (int i = 0; i < segment.Length; i++)
            {
                _diskMap.Add(segment);
            }
        }
    }

    public void DisplayDiskCharMap()
    {
        foreach (var segment in _diskMap)
        {
            Console.Write(segment);
        }

        Console.WriteLine();
    }

    public void DefragDiskMap()
    {
        for (var i = _diskMap.Count - 1; i >= 0; i--)
        {
            var segment = _diskMap[i];
            if (segment.IsEmpty)
                continue;

            // move to the first open slot that appears from the left
            var openSlotIndex = _diskMap.FindIndex(x => x.IsEmpty);
            if (openSlotIndex == -1 || openSlotIndex >= i)
                continue;

            _diskMap[i] = _diskMap[openSlotIndex];
            _diskMap[openSlotIndex] = segment;
            
        }
    }

    public void Defrag()
    {
        foreach (var segment in Segments.OrderByDescending(x => x.FileId))
        {
            // DisplayDiskMap();
            var emptySegmentIndex = Segments.FindIndex(x => x.IsEmpty && x.Length >= segment.Length && x.StartIndex < segment.StartIndex);
            if(emptySegmentIndex == -1)
                continue;
            
            var emptySegment = Segments[emptySegmentIndex];
            
            if(emptySegment.Length > segment.Length)
            {
                var newEmptySegment = new Segment
                {
                    StartIndex = emptySegment.StartIndex + segment.Length,
                    Length = emptySegment.Length - segment.Length,
                    FileId = -1
                };
                Segments.Insert(emptySegmentIndex + 1, newEmptySegment);
                MergeWithRightEmptySegments(emptySegmentIndex + 1);
            }
            
            emptySegment.Length = segment.Length;
            emptySegment.FileId = segment.FileId;
            
            segment.FileId = -1;
        }
        CreateDiskMap();
    }

    private void MergeWithRightEmptySegments(int emptySegmentIndex)
    {
        var rightEmptySegmentIndex = emptySegmentIndex + 1;
        while (rightEmptySegmentIndex < Segments.Count && Segments[rightEmptySegmentIndex].IsEmpty)
        {
            Segments[emptySegmentIndex].Length += Segments[rightEmptySegmentIndex].Length;
            Segments.RemoveAt(rightEmptySegmentIndex);
        }
    }
}