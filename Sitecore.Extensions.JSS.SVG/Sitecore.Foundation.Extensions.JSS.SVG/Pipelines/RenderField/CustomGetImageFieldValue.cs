using Sitecore.Pipelines.RenderField;
using Sitecore.Xml.Xsl;

namespace Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.RenderField
{
    public class CustomGetImageFieldValue : GetImageFieldValue
    {
        protected override ImageRenderer CreateRenderer()
        {
            return new CustomImageRenderer();
        }
    }
}