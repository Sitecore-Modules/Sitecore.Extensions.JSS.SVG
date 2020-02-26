using Sitecore;
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

        public override RenderFieldResult Render()
        {
            var obj = Item;
            if (obj == null)
                return RenderFieldResult.Empty;
            var parameters = Parameters;
            if (parameters == null)
                return RenderFieldResult.Empty;
            ParseNodeEx(parameters);
            var field = obj.Fields[FieldName];
            if (field != null)
            {
                _imageField = new ImageField(field, FieldValue);

                if (_imageField.MediaItem != null)
                {
                    var imageMediaItem = new MediaItem(_imageField.MediaItem);

                    if (imageMediaItem.MimeType == "image/svg+xml")
                    {
                        return new RenderFieldResult(RenderSvgImage(imageMediaItem));
                    }
                }
            }

            return base.Render();
        }

        protected virtual void ParseNodeEx(SafeDictionary<string> attributes)
        {
            Assert.ArgumentNotNull(attributes, nameof(attributes));
            _width = MainUtil.GetInt(Extract(attributes, ref _widthSet, "width", "w"), 0);
            _height = MainUtil.GetInt(Extract(attributes, ref _heightSet, "height", "h"), 0);
        }

        private string RenderSvgImage(MediaItem mediaItem)
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