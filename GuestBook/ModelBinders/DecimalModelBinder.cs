using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;
using ModelState = System.Web.Mvc.ModelState;


namespace GuestBook
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            const string decSeparatorComma=",";
            const string decSeparatorNeutral = ".";


            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                var strValue = valueResult.AttemptedValue;
                var decSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                if (decSeparator == decSeparatorComma)
                {
                    if (strValue.IndexOf(decSeparatorNeutral, System.StringComparison.Ordinal) >= 0)
                    {
                        strValue = strValue.Replace(decSeparatorNeutral, decSeparatorComma);
                    }
                }
                else
                {
                    if (strValue.IndexOf(decSeparatorComma, System.StringComparison.Ordinal) >= 0)
                    {
                        strValue = strValue.Replace(decSeparatorComma, decSeparatorNeutral);
                    }
                }

                actualValue = Convert.ToDecimal(strValue, CultureInfo.CurrentCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}