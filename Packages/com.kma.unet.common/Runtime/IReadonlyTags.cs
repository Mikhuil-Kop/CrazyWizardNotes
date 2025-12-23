using System.Collections.Generic;

namespace UNet.Common
{
    public interface IReadonlyTags : IEnumerable<string>
    {
        bool Has(string tag);
    }
}