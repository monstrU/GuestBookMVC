using System;
using System.Activities.Validation;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using DomainModel;
using GuestBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBook.FakedObject;

namespace TestBook
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void ModelValidateTestOk()
        {
            var model = new MessageModel();
            model.UpdateDate = DateTime.Now.AddDays(1);
            model.Title = "title";
            model.Body = "body";
            model.Weight = (decimal) 1;
            model.WeightDouble = (decimal?) 2.12;
            TestContextController controller = new TestContextController();
            var result = controller.TestTryValidateModel(model);

            Assert.IsTrue(result);

            var state = controller.ModelState;
            Assert.IsTrue(state.IsValid);


        }

        [TestMethod]
        public void ModelValidateTestFalse()
        {
            var model = new MessageModel();
            model.UpdateDate = DateTime.Now.AddDays(-1);
            model.Title = "title";
            model.Body = "body";
            TestContextController controller = new TestContextController();
            var result = controller.TestTryValidateModel(model);

            Assert.IsFalse(result);

            var state = controller.ModelState;
            Assert.IsFalse(state.IsValid);


        }

        [TestMethod]
        [Ignore]
        public void DecimalValidateTest()
        {

            var formCollection = new NameValueCollection { 
                    { "value", "2" }
            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(decimal));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "decimal",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };
            DecimalModelBinder b1 = new DecimalModelBinder();
            ControllerContext controllerContext = new ControllerContext();

            var result = (decimal)b1.BindModel(controllerContext, bindingContext);

            //Assert.IsFalse(state.IsValid);
        }

        [TestMethod]
        public void ModelValidateDecTestCommaInRussian()
        {
            var formCollection = new NameValueCollection { 
                    { "Weight", "2,12" }
            };


            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MessageModel));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Weight",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };
            Thread.CurrentThread.CurrentCulture = GetRussianCulture();

            ControllerContext controllerContext = new ControllerContext();
            DecimalModelBinder b1 = new DecimalModelBinder();
            var result = b1.BindModel(controllerContext, bindingContext);


            Assert.IsNotNull(result);
            Assert.IsTrue(result is decimal);
            Assert.IsTrue((decimal)result == (decimal)2.12);

            var state = bindingContext.ModelState;
            Assert.IsTrue(state.IsValid);

        }

        [TestMethod]
        public void ModelValidateDecTestPointInRussain()
        {
            var formCollection = new NameValueCollection { 
                    { "Weight", "2.12" }
            };


            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MessageModel));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Weight",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            Thread.CurrentThread.CurrentCulture = GetRussianCulture();
            ControllerContext controllerContext = new ControllerContext();
            DecimalModelBinder b1 = new DecimalModelBinder();
            var result = b1.BindModel(controllerContext, bindingContext);


            Assert.IsNotNull(result);
            Assert.IsTrue(result is decimal);
            Assert.IsTrue((decimal)result == (decimal)2.12);

            var state = bindingContext.ModelState;
            Assert.IsTrue(state.IsValid);

        }


        [TestMethod]
        public void ModelValidateDecTestPointNautral()
        {
            const string pointDecSeparator = ".";
            var formCollection = new NameValueCollection { 
                    { "Weight", "2.12" }
            };


            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MessageModel));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Weight",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            Thread.CurrentThread.CurrentCulture = GetNeutralCulture(pointDecSeparator);
            
            ControllerContext controllerContext = new ControllerContext();
            DecimalModelBinder b1 = new DecimalModelBinder();
            var result = b1.BindModel(controllerContext, bindingContext);


            Assert.IsNotNull(result);
            Assert.IsTrue(result is decimal);
            Assert.IsTrue((decimal)result == (decimal)2.12);

            var state = bindingContext.ModelState;
            Assert.IsTrue(state.IsValid);

        }


        [TestMethod]
        public void ModelValidateDecTestCommaNautral()
        {
            const string pointDecSeparator = ",";
            const string modelName = "Weight";
            var formCollection = new NameValueCollection { 
                    {modelName , "2,12" }
            };


            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MessageModel));

            var bindingContext = new ModelBindingContext
            {
                ModelName = modelName,
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            Thread.CurrentThread.CurrentCulture = GetNeutralCulture(pointDecSeparator);

            ControllerContext controllerContext = new ControllerContext();
            DecimalModelBinder b1 = new DecimalModelBinder();
            var result = b1.BindModel(controllerContext, bindingContext);


            Assert.IsNotNull(result);
            Assert.IsTrue(result is decimal);
            Assert.IsTrue((decimal)result == (decimal)2.12);

            var state = bindingContext.ModelState;
            Assert.IsTrue(state.IsValid);
            Assert.IsTrue(state.IsValidField(modelName));

        }

        [TestMethod]
        public void ModelValidateTestPointNautral()
        {
            const string pointDecSeparator = ".";
            const string modelName = "Weight";
            var formCollection = new NameValueCollection { 
                    { modelName, "2.12" }
            };


            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MessageModel));

            var bindingContext = new ModelBindingContext
            {
                ModelName = modelName,
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            
            Thread.CurrentThread.CurrentCulture = GetNeutralCulture(pointDecSeparator);

            ControllerContext controllerContext = new ControllerContext();
            DecimalModelBinder b1 = new DecimalModelBinder();
            var result = b1.BindModel(controllerContext, bindingContext);


            Assert.IsNotNull(result);
            Assert.IsTrue(result is decimal);
            Assert.IsTrue((decimal)result == (decimal)2.12);

            var state = bindingContext.ModelState;
            Assert.IsTrue(state.IsValid);
            Assert.IsTrue(state.IsValidField(modelName));
            

        }


        [TestMethod]
        public void ModelValidateDecTestFailRussian()
        {
            
            const string modelName = "Weight";
            var formCollection = new NameValueCollection { 
                    { modelName, "123qwe" }
            };


            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MessageModel));

            var bindingContext = new ModelBindingContext
            {
                ModelName = modelName,
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };


            Thread.CurrentThread.CurrentCulture = GetRussianCulture();

            ControllerContext controllerContext = new ControllerContext();
            DecimalModelBinder b1 = new DecimalModelBinder();
            var result = b1.BindModel(controllerContext, bindingContext);


            Assert.IsNull(result);
            var state = bindingContext.ModelState;
            Assert.IsFalse(state.IsValid);
            Assert.IsFalse(state.IsValidField(modelName));


        }


        [TestMethod]
        public void ModelValidateNullDecTestPointOkInRussain()
        {
            var formCollection = new NameValueCollection { 
                    { "WeightDouble", "2.12" }
            };


            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MessageModel));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "WeightDouble",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            Thread.CurrentThread.CurrentCulture = GetRussianCulture();

            ControllerContext controllerContext = new ControllerContext();
            DecimalModelBinder b1 = new DecimalModelBinder();
            var result = b1.BindModel(controllerContext, bindingContext);


            Assert.IsNotNull(result);
            Assert.IsTrue(result is decimal);
            Assert.IsTrue((decimal)result == (decimal)2.12);

            var state = bindingContext.ModelState;

            Assert.IsTrue(state.IsValid);
            Assert.IsTrue(state.IsValidField("WeightDouble"));
            
            

        }

        private CultureInfo GetNeutralCulture(string pointDecSeparator)
        {
            CultureInfo neutral = new CultureInfo("", true);
            neutral.NumberFormat.CurrencyDecimalSeparator = pointDecSeparator;
            return neutral;
        }

        private CultureInfo GetRussianCulture()
        {
            CultureInfo russian = new CultureInfo("ru-RU", true);
            return russian;
        }

    }
}
