﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlackBee.Toolkit.Rest.EasyData;
using BlackBee.Toolkit.Rest.EasyData.Classes;
using BS.Vms.JShoping.ImExPrice.DAL.Classes;

namespace BS.Vms.JShoping.ImExPrice.DAL
{
    public class ImExPriceDataContext : DataContext
    {
        private readonly string _pathGetProducts;

        public ImExPriceDataContext(string nameConnnect, string baseAddress, Encoding encoding) : base(nameConnnect,
            baseAddress, encoding)
        {
            _pathGetProducts = "/rest/products";
        }

        public async Task<List<Product>> API_GET_Products()
        {
            var resl = await Proxy.GetAsync<ResponceServer<Product>>(BaseAddress + _pathGetProducts);
            return resl.DataList;
        }
    }
}