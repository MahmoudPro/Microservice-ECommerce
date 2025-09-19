using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Constants
{
    public class ProductConstants
    {

        public const int MinNameLength = 3;
        public const int MaxNameLength = 100;
        public const int MinDescriptionLength = 5;
        public const int MaxDescriptionLength = 300;
        public const int MinSummaryLength = 5;
        public const int MaxSummaryLength = 200;
        public const decimal MinPrice = 0.01M;
        public const decimal MaxPrice = 10000M;
        public const int MaxImageFileLength = 100;

    }
}
