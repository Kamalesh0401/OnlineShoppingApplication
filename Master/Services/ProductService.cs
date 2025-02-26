﻿using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;

namespace Master.Services
{
    public class ProductService : IProductService
    {
        #region Constructor
        //public ProductService(IServiceProvider serviceProvider, ILogger<ProductService> logger) { }

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<List<ProductObject>> GetAllProduct(SessionInfo sessionInfo, ProductObject input)
        {
            var output = new List<ProductObject>();
            try
            {
                output = await _repository.GetAllProductsAsync(input);

                //var dbParameters = new DBParameters();
                //dbParameters.Add("@img_dir", input.img_dir);
                //dbParameters.Add("@prod_id", input.prod_id);
                //dbParameters.Add("@supp_map_id", input.supp_map_id);
                //dbParameters.Add("@lp_prod_typ", input.lp_prod_typ);
                ////SSM009 onload.retrives image details from the wkg_img_dtls for specified prod_id and supp_map_id
                //string query = @"SELECT CONCAT((SELECT ftp_supp_img_url FROM wkg_cntrl_param_mast), 
                //                  LOWER(img.img_dir), '/', img.img_nam) AS img_url,img_srl,img_nam,img_dir,is_video,mnl_upld
                //                  FROM wkg_img_dtls img WHERE img_dir = @img_dir AND 
                //                  (prod_id = @prod_id OR COALESCE(prod_id, '') = '') AND 
                //                  (supp_map_id = @supp_map_id OR COALESCE(supp_map_id, '') = '') AND 
                //                  (lp_prod_typ = @lp_prod_typ OR COALESCE(lp_prod_typ, '') = '') 
                //                  ORDER BY ISNULL(sort_ordr, '');";

                //DataSet DS = await this.DBUtils(true).GetDataSetAsync(query, dbParameters);
                //if (DS != null && DS.Tables.Count > 0)
                //{
                //    GetObjImg = new List<ProductObject>();
                //    foreach (DataRow r in DS.Tables[0].Rows)
                //    {
                //        SSM009Object ImgObj = new ProductObject();
                //        ImgObj.img_nam = r.GetValue<string>("img_nam");
                //        ImgObj.img_srl = r.GetValue<string>("img_srl");
                //        ImgObj.img_dir = r.GetValue<string>("img_dir");
                //        ImgObj.img_url = r.GetValue<string>("img_url");
                //        ImgObj.img_Ftp_url = null;
                //        ImgObj.is_video = r.GetValue<bool>("is_video");
                //        ImgObj.mnl_upld = r.GetValue<bool>("mnl_upld");

                //        GetObjImg.Add(ImgObj);
                //    }
                //}
                //output = GetObjImg;
            }
            catch (Exception ex)
            {
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

            }
            return output;
        }

        public async Task<OperationStatus> AddProduct(SessionInfo sessionInfo, ProductObject input)
        {
            return await _repository.AddProductAsync(input);

        }
        #endregion

    }
}
