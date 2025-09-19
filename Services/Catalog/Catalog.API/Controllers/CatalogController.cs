using System.Net;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class CatalogController : BaseAPIController
    {
        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductReponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductReponseDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductsByName")]
        [ProducesResponseType(typeof(IList<ProductReponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductReponseDto>>> GetProductsByName(string productName)
        {
            var query = new GetProductsByNameQuery(productName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]", Name = "GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductReponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductReponseDto>>> GetAllProducts([FromQuery] CatalogSpecParams spec)
        {
            var query = new GetAllProductQuery(spec);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]", Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductReponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReponseDto>> CreateProduct([FromBody] CreateProductCommand createProduct)
        {
            var result = await _mediator.Send(createProduct);
            // OR
            //var result = await _mediator.Send<ProductReponseDto>(createProduct);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand updateProduct)
        {
            var result = await _mediator.Send(updateProduct);
            return Ok(result);
        }

        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]", Name = "GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandReponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<BrandReponseDto>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]", Name = "GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<TypeResponseDto>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
