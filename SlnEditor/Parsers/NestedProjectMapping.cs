using System;

namespace SlnEditor.Parsers
{
    internal class NestedProjectMapping
    {
        public NestedProjectMapping(
            string targetId,
            string destinationId)
        {
            TargetId = new Guid(targetId);
            DestinationId = new Guid(destinationId);
        }

        public Guid TargetId { get; }

        public Guid DestinationId { get; }
    }
}
