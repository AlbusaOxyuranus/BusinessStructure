using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackBee.Toolkit.Rest.EasyData;
using BusinessStructure.Bc;
using BusinessStructure.Vms.JShoping.ImExPrice.BAL.Models;
using BusinessStructure.Vms.JShoping.ImExPrice.DAL;

namespace BusinessStructure.Vms.JShoping.ImExPrice.BAL
{
    internal class ImExPriceBusinessContext : BussinessContext
    {
        public ImExPriceBusinessContext(IModeApp modeApp) : base(modeApp)
        {
            DataContext = new ImExPriceDataContext(modeApp.ConnectionBase.ToString(), modeApp.AdressConnection,
                Encoding.UTF8);
            ProductsContext = new ProductsContext(DataContext);
        }

        public ProductsContext ProductsContext { get; set; }

        public async Task<List<ProductModel>> GetProducts()
        {
            return await ProductsContext.GetProducts();
        }

        public async Task<List<ExtraFieldsValuesModel>> GetExtraFieldsValuesAsync()
        {
            return await ProductsContext.GetExtraFieldsValuesAsync();
        }

        public async Task<List<ClientModel>> GetClientsAsync()
        {
            var data = await(DataContext as ImExPriceDataContext).API_GET_Orders_Client();
            var list = data.Select(l => new ClientModel
            {
                FirstName=l.FirstName,LastName=l.LastName, Email=l.Email,Street=l.Street,
                City=l.City,Phone=l.Phone
            }).ToList();
            return list;
        }
    }

    public class ProductsContext
    {
        public ProductsContext(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IDataContext DataContext { get; }

        //public Task<List<ProductModel>> GetExtraFields()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<List<ExtraFieldsValuesModel>> GetExtraFieldsValuesAsync()
        //{
        //    await Task.Delay(100);
        //    var data = await (DataContext as ImExDataContext).API_GET_ExtraFieldsValues();
        //    var list = data.Select(l => new ExtraFieldsValuesModel() { Id = l.Id, FieldId = l.FieldId, Ordering = l.Ordering, NameRu = l.NameRu }).ToList();
        //    return list;
        //}

        public async Task<List<ProductModel>> GetProducts()
        {
            
            var data = await (DataContext as ImExPriceDataContext).API_GET_Products();
            var list = data.Select(l => new ProductModel
            {
                Id = l.ProductId,
                ProductEan = l.ProductEan,
                Name = l.Name,
                ProductPrice = l.ProductPrice,
                ProductQuantity = l.ProductQuantity,
                WeightVolumeUnits = l.WeightVolumeUnits,
                ProductPublish = l.ProductPublish,
                ImageUrl = l.Image,
                ExtraField1 = l.ExtraField1,
                ExtraField2 = l.ExtraField2
            }).ToList();
            return list;
        }

        public async Task<List<ExtraFieldsValuesModel>> GetExtraFieldsValuesAsync()
        {
            
            var data = await (DataContext as ImExPriceDataContext).API_GET_ExtraFieldsValues();
            var list = data.Select(l =>
                    new ExtraFieldsValuesModel
 {
                        Id = l.Id,
                        FieldId = l.FieldId,
                        Ordering = l.Ordering,
                        NameRu = l.NameRu
                    })
                .ToList();
            return list;
        }
    }
}