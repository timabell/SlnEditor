using SlnEditor.Models;
using System;

namespace SlnEditor.Parsers
{
    internal interface IProjectTypeMapper
    {
        ProjectType Map(Guid typeGuid);
    }
}
