using Sitecore.LayoutService.Serialization;
using System;
using System.Collections.Generic;

namespace Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.ImageFieldSerializer
{
    /// <summary>
    /// Implements the the Custom Image Field Serializer for serializing the SVG images.
    /// Inherits the <see cref="P:Sitecore.LayoutService.Serialization.FieldSerializers.ImageFieldSerializer"/>.
    /// </summary>
    class CustomImageFieldSerializer : LayoutService.Serialization.FieldSerializers.ImageFieldSerializer
    {

        public CustomImageFieldSerializer(IFieldRenderer fieldRenderer) : base(fieldRenderer)
        {
        }

        /// <summary>
        /// Overrides ParseRenderedImage method of the <see cref="P:Sitecore.LayoutService.Serialization.FieldSerializers.ImageFieldSerializer"/>.
        /// Parses the rendered field to Dictionary.
        /// If the rendered field has the SVG code, it will put this code into the dictionary with "svgCode" key.
        /// Otherwise this method will call the base method.
        /// </summary>
        /// <param name="renderedField">The rendered field.</param>
        /// <returns>The rendered field as Dictionary.</returns>
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
    }
}