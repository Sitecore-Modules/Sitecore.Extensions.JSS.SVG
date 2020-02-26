using Sitecore.Pipelines.RenderField;
using Sitecore.Xml.Xsl;

namespace Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.RenderField
{
    /// <summary>
    /// Renders the image from <see cref="P:Sitecore.Pipelines.RenderField.RenderFieldArgs.Item" /> into <see cref="P:Sitecore.Pipelines.RenderField.RenderFieldArgs.Result" />.
    /// Inherits the <see cref="P:Sitecore.Pipelines.RenderField.GetImageFieldValue"/> and returns the <see cref="T:Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.RenderField.CustomImageRenderer" />
    /// </summary>
    public class CustomGetImageFieldValue : GetImageFieldValue
    {
        /// <summary>
        /// Creates the new instance of the <see cref="T:Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.RenderField.CustomImageRenderer" /> class that will do the rendering part.
        /// </summary>
        /// <returns>The renderer.</returns>
        protected override ImageRenderer CreateRenderer()
        {
            return new CustomImageRenderer();
        }
    }
}