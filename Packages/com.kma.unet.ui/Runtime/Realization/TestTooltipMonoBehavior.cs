using UNet.UI.Abstractions;
using UnityEngine;

namespace UNet.UI.Realization
{
    public class TestTooltipMonoBehavior : MonoBehaviour, IHasTooltipInfo
    {
        public string Title;
        public string Description;

        public TooltipInfo GetTooltipInfo()
        {
            return new TooltipInfo()
            {
                Title = Title,
                Description = Description
            };
        }
    }
}
