using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackBee.Toolkit.Rest.EasyData;
using BS.Bc;
using BS.Vms.JShoping.ImExPrice.BAL.Models;
using BS.Vms.JShoping.ImExPrice.DAL;

namespace BS.Vms.JShoping.ImExPrice.BAL
{
    internal class ImExPriceBusinessContext : BussinessContext
    {
        public ImExPriceBusinessContext(IModeApp modeApp) : base(modeApp)
        {
            DataContext = new ImExPriceDataContext(modeApp.ConnectionBase.ToString(), modeApp.AdressConnection, Encoding.UTF8);
            this.ProductsContext = new ProductsContext(DataContext);
        }

        public ProductsContext ProductsContext { get; set; }

        public async Task<List<ProductModel>> GetProducts()
        {
            return await ProductsContext.GetProducts();
        }
    }

    public class ProductsContext
    {
        public ProductsContext(IDataContext dataContext)
        {
            this.DataContext = dataContext;
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
            await Task.Delay(100);
            var data = await (DataContext as ImExPriceDataContext).API_GET_Products();
            var list = data.Select(l => new ProductModel() { Id = l.ProductId, ProductEan = l.ProductEan, Name = l.Name, ProductPrice = l.ProductPrice, ProductQuantity = l.ProductQuantity, WeightVolumeUnits = l.WeightVolumeUnits, ProductPublish = l.ProductPublish, ImageUrl = l.Image}).ToList();
            return list;
        }
    }
}