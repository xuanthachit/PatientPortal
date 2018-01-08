﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using PatientPortal.IRepository.CMS;
using PatientPortal.Repositoty.CMS;
using PatientPortal.DataAccess.CMS;
using PatientPortal.DataAccess.Repo.CMS;
using PatientPortal.IRepository.Wrapper;
using PatientPortal.Repositoty.Wraper;
using PatientPortal.DataAccess.Wrapper;
using PatientPortal.DataAccess.Repo.Wrapper;
using Autofac.Integration.WebApi;
using PatientPortal.Repositoty.Authorize;
using PatientPortal.IRepository.Authorize;
using PatientPortal.DataAccess.Authorize;
using PatientPortal.DataAccess.Repo.Authorize;
using PatientPortal.API.CMS.Models;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace PatientPortal.API.CMS
{
    public class AutofacConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = System.Web.Http.GlobalConfiguration.Configuration;

            // Register your Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);
            builder.RegisterType<CategoryRepoImpl>().As<ICategoryRepo>().InstancePerRequest();
            builder.RegisterType<CategoryImpl>().As<ICategory>().InstancePerRequest();

            //Workflow
            builder.RegisterType<WorkflowRepoImpl>().As<IWorkflowRepo>().InstancePerRequest();
            builder.RegisterType<WorkflowImpl>().As<IWorkflow>().InstancePerRequest();

            //Language
            builder.RegisterType<LanguageRepoImpl>().As<ILanguageRepo>().InstancePerRequest();
            builder.RegisterType<LanguageImpl>().As<ILanguage>().InstancePerRequest();

            //WorkflowState
            builder.RegisterType<WorkflowStateRepoImpl>().As<IWorkflowStateRepo>().InstancePerRequest();
            builder.RegisterType<WorkflowStateImpl>().As<IWorkflowState>().InstancePerRequest();

            //WorkflowState
            builder.RegisterType<WorkflowNavigationRepoImpl>().As<IWorkflowNavigationRepo>().InstancePerRequest();
            builder.RegisterType<WorkflowNavigationImpl>().As<IWorkflowNavigation>().InstancePerRequest();

            // Post
            builder.RegisterType<PostRepoImpl>().As<IPostRepo>().InstancePerRequest();
            builder.RegisterType<PostImpl>().As<IPost>().InstancePerRequest();

            // Links
            builder.RegisterType<LinkBuildingRepoImpl>().As<ILinkBuildingRepo>().InstancePerRequest();
            builder.RegisterType<LinkBuildingImpl>().As<ILinkBuilding>().InstancePerRequest();

            // Feature
            builder.RegisterType<FeatureRepoImpl>().As<IFeatureRepo>().InstancePerRequest();
            builder.RegisterType<FeatureImpl>().As<IFeature>().InstancePerRequest();

            // Configuration
            builder.RegisterType<ConfigurationRepoImpl>().As<IConfigurationRepo>().InstancePerRequest();
            builder.RegisterType<ConfigurationImpl>().As<IConfiguration>().InstancePerRequest();

            // Slider
            builder.RegisterType<SliderImpl>().As<ISlider>().InstancePerRequest();
            builder.RegisterType<SliderRepoImpl>().As<ISliderRepo>().InstancePerRequest();

            //Advertise
            builder.RegisterType<AdvertiseImpl>().As<IAdvertise>().InstancePerRequest();
            builder.RegisterType<AdvertiseRepoImpl>().As<IAdvertiseRepo>().InstancePerRequest();

            //AdapterPattern
            builder.RegisterType<AdapterPattern>().As<IAdapterPattern>().InstancePerRequest();
            builder.RegisterType<AdapterPatternRepoImpl>().As<IAdapterPatternRepo>().InstancePerRequest();

            //Gallery
            builder.RegisterType<GalleryImpl>().As<IGallery>().InstancePerRequest();
            builder.RegisterType<GalleryRepoImpl>().As<IGalleryRepo>().InstancePerRequest();

            builder.RegisterType<GalleryStoreImpl>().As<IGalleryStore>().InstancePerRequest();
            builder.RegisterType<GalleryStoreRepoImpl>().As<IGalleryStoreRepo>().InstancePerRequest();


            //User Authorization
            builder.RegisterType<UserAuthorizationRepoImpl>().As<IUserAuthorizationRepo>().InstancePerRequest();
            builder.RegisterType<UserAuthorizationImpl>().As<IUserAuthorization>().InstancePerRequest();

            //Redis Cache
            builder.RegisterType<NewtonsoftSerializer>().As<ISerializer>().InstancePerRequest();
            builder.RegisterType<StackExchangeRedisCacheClient>().As<ICacheClient>().InstancePerRequest();

            builder.RegisterType<AuthorizeRolesAttribute>().PropertiesAutowired();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }

}