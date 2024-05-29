using System;
using System.Collections.Generic;

public class Terrain
{
    public int Elevation { get; set; }
    public double SpeedReductionFactor { get; set; }

    public Terrain(int elevation, double speedReductionFactor)
    {
        if (elevation < 0)
            throw new ArgumentException("Elevation must be non-negative.", nameof(elevation));
        if (speedReductionFactor <= 0 || speedReductionFactor >= 1)
            throw new ArgumentException("Speed reduction factor must be in the range (0, 1).", nameof(speedReductionFactor));

        Elevation = elevation;
        SpeedReductionFactor = speedReductionFactor;
    }
}

public class Meadow : Terrain
{
    public string GrassColor { get; set; }

    public Meadow(int elevation, double speedReductionFactor, string grassColor)
        : base(elevation, speedReductionFactor)
    {
        if (string.IsNullOrWhiteSpace(grassColor))
            throw new ArgumentException("Grass color cannot be null or whitespace.", nameof(grassColor));

        GrassColor = grassColor;
    }
}

public class Water : Terrain
{
    public int Depth { get; set; }

    public Water(int elevation, double speedReductionFactor, int depth)
        : base(elevation, speedReductionFactor)
    {
        if (depth < 0)
            throw new ArgumentException("Depth must be non-negative.", nameof(depth));

        Depth = depth;
    }
}

public class Map
{
    private List<Terrain> _fields = new List<Terrain>();

    public void AddField(Terrain field)
    {
        _fields.Add(field);
    }

    public int CountDeepWaterFields(int minDepth)
    {
        return _fields.Count(f => f is Water water && water.Depth > minDepth);
    }
}

class Program
{
    static void Main()
    {
        var map = new Map();

        try
        {
            map.AddField(new Meadow(100, 0.9, "Green"));
            map.AddField(new Water(0, 0.99, 10));
            map.AddField(new Meadow(150, 0.85, "Brown"));
            map.AddField(new Water(0, 0.98, 7));
            map.AddField(new Meadow(200, 0.8, "Blue"));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }

        Console.WriteLine($"Количество полей воды глубже {5}: {map.CountDeepWaterFields(5)}");
    }
}