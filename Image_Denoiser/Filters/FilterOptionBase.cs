using System.Drawing;

namespace PROTO
{
    internal abstract class FilterOptionBase
    {
        public abstract string FilterName { get; }
        internal abstract Image FitlerImage(Image inputImage);
        public override string ToString()
        {
            return FilterName;
        }
    }
}
