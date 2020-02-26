using Sitecore.Diagnostics;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;

namespace Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.ImageFieldSerializer
{
    /// <summary>
    /// Serialize the image field from <see cref="P:Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer.GetFieldSerializerPipelineArgs.Field" /> into <see cref="P:Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer.GetFieldSerializerPipelineArgs.Result" />.
    /// Inherits the <see cref="P:Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer.CustomGetImageFieldSerializer"/> and returns the <see cref="T:Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.ImageFieldSerializer.CustomImageFieldSerializer" />.
    /// </summary>
    public class CustomGetImageFieldSerializer : GetImageFieldSerializer
    {
        public CustomGetImageFieldSerializer(IFieldRenderer fieldRenderer) : base(fieldRenderer)
        {
        }

        /// <summary>
        /// Sets the result of serialization into <see cref="P:Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer.GetFieldSerializerPipelineArgs.Result" />.
        /// </summary>
        /// <param name="args">The arguments specify what field to serialize.</param>
        protected override void SetResult(GetFieldSerializerPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            args.Result = new CustomImageFieldSerializer(FieldRenderer);
        }
    }
}