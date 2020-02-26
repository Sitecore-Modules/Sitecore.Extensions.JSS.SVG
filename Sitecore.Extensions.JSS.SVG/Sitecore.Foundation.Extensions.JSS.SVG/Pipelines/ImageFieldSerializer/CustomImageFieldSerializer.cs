using Newtonsoft.Json;
using Sitecore.Data.Fields;
using Sitecore.LayoutService.Serialization;
using System;
using System.Collections.Generic;

namespace Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.ImageFieldSerializer
{
    class CustomImageFieldSerializer : LayoutService.Serialization.FieldSerializers.ImageFieldSerializer
    {
        string _renderedValue;
        public CustomImageFieldSerializer(IFieldRenderer fieldRenderer) : base(fieldRenderer)
        {
        }

        protected override IDictionary<string, string> ParseRenderedImage(string renderedField)
        {
            var dictionary = new Dictionary<string, string>();

            var startSvgTag = "<svg";
            var endSvgTag = "/svg>";

            var startIndex = renderedField.IndexOf(startSvgTag, StringComparison.Ordinal);
            if (startIndex != -1)
            {
                var lastIndex = renderedField.LastIndexOf(endSvgTag, StringComparison.Ordinal);

                var result = renderedField.Substring(startIndex, lastIndex - startIndex + endSvgTag.Length);

                dictionary.Add("svgCode", result);
                return dictionary;
            }

            return base.ParseRenderedImage(renderedField);
        }

        protected override void WriteValue(Field field, JsonTextWriter writer)
        {
            WriteImageObject(ParseRenderedImage(GetRenderedValue(field, null)), field, writer);
        }

        protected override string GetRenderedValue(Field field, SerializationOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(_renderedValue))
                _renderedValue = RenderField(field, options != null && options.DisableEditing).ToString();
            return _renderedValue;
        }
    }
}