using EDCC.Models;

namespace EDCC.Fill;

public class ScheduleFill
{
    public string Name { get; set; }
    
    public List<Lesson> Lessons { get; set; }
}