using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Catalog.Core.Constants;

namespace Catalog.Core.Entities
{
    public class Product: BaseEntity
    {
        /// <summary>
        ///  < see cref="NameConstant.MinNameLength" />
        ///  < see cref="NameConstant.MaxNameLength" />
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  < see cref="ProductConstants.MinDescriptionLength" />
        ///  < see cref="ProductConstants.MaxDescriptionLength" />
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  < see cref="ProductConstants.MinSummaryLength" />
        ///  < see cref="ProductConstants.MaxSummaryLength" />
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///  < see cref="ProductConstants.MaxImageFileLength" />
        /// </summary>
        public string ImageFile { get; set; }

        /// <summary>
        ///  < see cref="ProductConstants.MinPrice" />
        ///  < see cref="ProductConstants.MaxPrice" />
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }

        public ProductBrand Brand { get; set; }

        public ProductType Type { get; set; }
    }
}
