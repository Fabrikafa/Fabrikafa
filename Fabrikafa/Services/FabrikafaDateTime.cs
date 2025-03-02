using System;

namespace Fabrikafa.Services;

public class SystemDateTime : IDateTime
{
    public DateTime Now
    {
        get { return DateTime.UtcNow; }
    }
}
