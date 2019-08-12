using DotNetCoreWebApi.Framework.Forms;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Framework.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Controllers
{
    //[Route("/[controller]")]
    [ApiController]
    public abstract class ReadOnlyController<T, TEntity, TMapping, TResponseModel> : ControllerBase where TResponseModel : PagedCollection<T>, new()
    {
        private readonly IBaseService<T, TEntity> _baseService;
        private readonly PagingOptions _defaultPagingOptions;

        abstract protected Dictionary<string, Link> relations { get; }

        protected ReadOnlyController(IOptions<PagingOptions> defaultPagingOptionsWrapper, IBaseService<T, TEntity> baseService)
        {
            _baseService = baseService;
            _defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }

        // GET /rooms
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<T>>> GetAll(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<T, TEntity> sortOptions,
            [FromQuery] SearchOptions<T, TEntity> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var response = await _baseService.GetAllAsync(pagingOptions, sortOptions, searchOptions);

            var collection = PagedCollection<T>.Create<TResponseModel>(
                Link.ToCollection(nameof(GetAll)),
                response.Items.ToArray(),
                response.TotalSize,
                pagingOptions);

            if(relations != null && relations.Any())
            {
                var linkProps = typeof(TResponseModel).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                              .Where(x => x.PropertyType == typeof(Link) && relations.ContainsKey(x.Name));

                foreach (var prop in linkProps)
                {
                    PropertyInfo propertyInfo = collection.GetType().GetProperty(prop.Name);
                    propertyInfo.SetValue(collection,
                                          relations[prop.Name]// Link.ToForm(nameof(GetAll),
                                         , null);
                }
            }

            var props = typeof(TResponseModel).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                              .Where(X => X.PropertyType == typeof(Form));

            foreach(var prop in props)
            {
                PropertyInfo propertyInfo = collection.GetType().GetProperty(prop.Name);
                propertyInfo.SetValue(collection, 
                                      Convert.ChangeType(FormMetadata.FromResource<T>(Link.ToForm(nameof(GetAll),
                                            null,
                                            Link.GetMethod,
                                            Form.QueryRelation))
                                     , prop.PropertyType)
                                     , null);
            }

            return collection;
        }

        //GET /rooms/{roomId}
        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<T>> GetById(Guid id)
        {
            var t = await _baseService.GetByIdAsync(id);

            if (t == null)
            {
                return NotFound();
            }

            return t;
        }
    }
}
