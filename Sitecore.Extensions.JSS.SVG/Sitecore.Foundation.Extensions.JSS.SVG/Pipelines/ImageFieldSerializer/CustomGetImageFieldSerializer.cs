using Sitecore.Diagnostics;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;

namespace Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.ImageFieldSerializer
{
    public class CustomGetImageFieldSerializer : GetImageFieldSerializer
    {
        public CustomGetImageFieldSerializer(IFieldRenderer fieldRenderer) : base(fieldRenderer)
        {
        }

        protected override void SetResult(GetFieldSerializerPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            args.Result = new CustomImageFieldSerializer(FieldRenderer);
        }
    }
}