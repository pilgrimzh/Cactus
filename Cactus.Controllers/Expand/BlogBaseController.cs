﻿using Cactus.Common;
using Cactus.IService.CMS;
using Cactus.Model.CMS;
using Cactus.Model.Sys.Enums;
using HTools;
using System;
using System.IO;
using System.Web.Mvc;

namespace Cactus.Controllers.Expand
{
    //基础站点信息
    public class BlogBaseController : BaseController
    {

        public IBlogConfigService blogConfigService = IocHelper.AutofacResolveNamed<IBlogConfigService>("CMS.BlogConfigService");

        /// <summary>
        /// 站点配置信息
        /// </summary>
        public BlogConfig blogConfig = null;
        /// <summary>
        /// 执行前读取站点配置信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            CacheObj obj = cacheService.Get(Constant.CacheKey.BlogConfigCacheKey);
            this.blogConfig = (obj != null && obj.value != null) ? (obj.value as BlogConfig) : null;
            if (this.blogConfig == null)
            {
                this.blogConfig = blogConfigService.LoadConfig(Constant.BlogConfigPath);
                cacheService.Add(Constant.CacheKey.BlogConfigCacheKey,
                    new CacheObj()
                    {
                        value = blogConfig,
                        AbsoluteExpiration = new TimeSpan(1,0,0,0)
                    });
            }
            if (this.blogConfig != null)
            {
                ViewData["_BlogConfig"] = this.blogConfig;
            }
        }

        /// <summary>
        /// 返回结果前附加数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        

    }
}