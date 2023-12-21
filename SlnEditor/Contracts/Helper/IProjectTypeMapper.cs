using System;

namespace SlnEditor.Contracts.Helper
{
    internal interface IProjectTypeMapper
    {
        ProjectType Map(Guid typeGuid);
    }
}
