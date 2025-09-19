using Catalog.Core.Constants;

namespace Catalog.Core.Entities
{
    public class ProductBrand: BaseEntity
    {
        /// <summary>
        ///     < see cref="NameConstant.MinNameLength" />
        ///     < see cref="NameConstant.MaxNameLength" />
        /// </summary>
        public string Name { get; set; }
    }
}
