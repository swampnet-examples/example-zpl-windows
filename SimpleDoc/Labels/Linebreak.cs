using System;

namespace SimpleDoc.Labels
{
    public class Linebreak : ContentBase
    {
        public override string Emit(Section section)
        {
            // @TODO: Move cursor down
            // Need to move down based on the highest 'h' value we've already emitted for this section? Nggg...
            return Environment.NewLine;
        }
    }
}
