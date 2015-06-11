namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;

    public class ModelBinder : DefaultModelBinder
    {
        protected IUnityContainer Container
        {
            get;
            private set;
        }

        private readonly ModelMetadataProvider provider = new DataAnnotationsModelMetadataProvider();

        public ModelBinder(IUnityContainer container)
        {
            Container = container;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType.IsInterface)
            {
                var model = Container.Resolve(bindingContext.ModelType);

                if (model != null)
                {
                    var containerType = bindingContext.ModelMetadata.ContainerType;
                    var propertyName = bindingContext.ModelMetadata.PropertyName;
                    var metaData = new ModelMetadata(provider, containerType, null, model.GetType(),
                                                     propertyName) { Model = model };
                    bindingContext.ModelMetadata = metaData;
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }


    }
}
