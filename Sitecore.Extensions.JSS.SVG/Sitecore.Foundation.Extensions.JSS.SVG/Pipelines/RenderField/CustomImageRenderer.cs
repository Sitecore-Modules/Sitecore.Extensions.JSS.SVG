using System;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Xml.Xsl;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Sitecore.Foundation.Extensions.JSS.SVG.Pipelines.RenderField
{
    /// <summary>
    /// Implements the the Custom Image Renderer for rendering the SVG images.
    /// Inherits the <see cref="P:Sitecore.Xml.Xsl.ImageRenderer"/>.
    /// </summary>
    public class CustomImageRenderer : ImageRenderer
    {
        /// <summary>Image field.</summary>
        private ImageField _imageField;
        /// <summary>The width.</summary>
        private int _width;
        /// <summary>The height.</summary>
        private int _height;
        /// <summary>The width set.</summary>
        private bool _widthSet;
        /// <summary>The height set.</summary>
        private bool _heightSet;

        /// <summary>
        /// Overrides Render method of the <see cref="P:Sitecore.Xml.Xsl.ImageRenderer"/>.
        /// Checks the MIME type of the image field.
        /// If the MIME type corresponds to the SVG, it will render the field in the SVG code.
        /// Otherwise, it calls the base render method.
        /// </summary>
        /// <returns>The render.</returns>
        public override RenderFieldResult Render()
        {
            if (Item == null || Parameters == null)
            {
                return RenderFieldResult.Empty;
            }

            var field = Item.Fields[FieldName];
            if (field != null)
            {
                _imageField = new ImageField(field, FieldValue);

                if (_imageField.MediaItem != null)
                {
                    var imageMediaItem = new MediaItem(_imageField.MediaItem);

                    if (imageMediaItem.MimeType.Equals("image/svg+xml", StringComparison.Ordinal))
                    {
                        ParseNodeForSvg(Parameters);
                        return new RenderFieldResult(RenderSvgImage(imageMediaItem));
                    }
                }
            }

            return base.Render();
        }

        /// <summary>
        /// Extracts the specified attributes for SVG.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        protected virtual void ParseNodeForSvg(SafeDictionary<string> attributes)
        {
            Assert.ArgumentNotNull(attributes, nameof(attributes));
            _width = MainUtil.GetInt(Extract(attributes, ref _widthSet, "width", "w"), 0);
            _height = MainUtil.GetInt(Extract(attributes, ref _heightSet, "height", "h"), 0);
        }

        /// <summary>
        /// Extracts the svg code from the Media Item.
        /// </summary>
        /// <param name="mediaItem">The Media Item that represents SVG.</param>
        /// <returns>The SVG code as string.</returns>
        protected virtual string RenderSvgImage(MediaItem mediaItem)
        {
            Assert.ArgumentNotNull(mediaItem, "mediaItem");
            string result;

            using (var reader = new StreamReader(mediaItem.GetMediaStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            var svg = XDocument.Parse(result);

            if (svg.Document?.Root == null) return result;

            if (_width > 0)
            {
                svg.Document.Root.SetAttributeValue("width", _width);
            }

            if (_height > 0)
            {
                svg.Document.Root.SetAttributeValue("height", _height);
            }

            result = svg.ToString();

            return result;
        }

        /// <summary>
        /// Extracts a value from a dictionary and removes it. Also modifies flag wherever value was setted.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="valueSet">
        /// if set to <c>true</c> this instance is value was setted.
        /// </param>
        /// <param name="keys">The keys to extract.</param>
        /// <returns>The extracted key as string.</returns>
        private string Extract(SafeDictionary<string> values, ref bool valueSet, params string[] keys)
        {
            Assert.ArgumentNotNull(values, nameof(values));
            Assert.ArgumentNotNull(keys, nameof(keys));
            var str = Extract(values, keys);
            valueSet = str != null;
            return str;
        }
    }
}