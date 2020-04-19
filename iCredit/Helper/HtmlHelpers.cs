using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;



namespace CrediAdmin.Helper
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string itemText, string actionName, string controllerName, MvcHtmlString[] childElements = null)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            string finalHtml;
            var linkBuilder = new TagBuilder("a");
            var liBuilder = new TagBuilder("li");

            if (childElements != null && childElements.Length > 0)
            {
                linkBuilder.MergeAttribute("href", "#");
                linkBuilder.AddCssClass("dropdown-toggle");
                linkBuilder.InnerHtml = itemText + " <b class=\"caret\"></b>";
                linkBuilder.MergeAttribute("data-toggle", "dropdown");
                var ulBuilder = new TagBuilder("ul");
                ulBuilder.AddCssClass("dropdown-menu");
                ulBuilder.MergeAttribute("role", "menu");
                foreach (var item in childElements)
                {
                    ulBuilder.InnerHtml += item + "\n";
                }

                liBuilder.InnerHtml = linkBuilder + "\n" + ulBuilder;
                liBuilder.AddCssClass("dropdown");
                if (controllerName == currentController)
                {
                    liBuilder.AddCssClass("active");
                }

                finalHtml = liBuilder.ToString() + ulBuilder;
            }
            else
            {
                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);
                linkBuilder.MergeAttribute("href", urlHelper.Action(actionName, controllerName));
                linkBuilder.SetInnerText(itemText);
                liBuilder.InnerHtml = linkBuilder.ToString();
                if (controllerName == currentController && actionName == currentAction)
                {
                    liBuilder.AddCssClass("active");
                }

                finalHtml = liBuilder.ToString();
            }

            return new MvcHtmlString(finalHtml);
        }



        //public static MvcHtmlString RadioButtonListFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, string ulClass = null)
        //{
        //    ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

        //    var name = ExpressionHelper.GetExpressionText(expression);
        //    string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

        //    var html = new TagBuilder("ul");
        //    if (!String.IsNullOrEmpty(ulClass))
        //        html.MergeAttribute("class", ulClass);

        //    string innerhtml = "";

        //    Dictionary<string, T> myEnumDic = null;
        //    Dictionary<string, bool> myBoolDic = null;
        //    //
        //    if (typeof(T).BaseType == typeof(Enum))
        //    {
        //        myEnumDic = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(currentItem => Enum.GetName(typeof(T), currentItem));
        //        innerhtml = RadioRow<TModel, T>(htmlHelper, fullName, innerhtml, myEnumDic);
        //    }
        //    else if (typeof(T) == typeof(bool))
        //    {
        //        myBoolDic = new Dictionary<string, bool>();
        //        myBoolDic.Add("Yes", true);
        //        myBoolDic.Add("No", false);
        //        innerhtml = RadioRow<TModel, bool>(htmlHelper, fullName, innerhtml, myBoolDic);
        //    }
        //    html.InnerHtml = htmlHelper.Label(fullName).ToString() + innerhtml;
        //    return new MvcHtmlString(html.ToString());
        //}

        //private static string RadioRow<TModel, T>(HtmlHelper<TModel> htmlHelper, string fullName, string innerhtml, Dictionary<string, T> myDic)
        //{
        //    foreach (var item in myDic)
        //    {
        //        var liBuilder = new TagBuilder("li");
        //        liBuilder.InnerHtml = item.Key + " " + htmlHelper.RadioButton(fullName, item.Value).ToString();
        //        innerhtml = innerhtml + liBuilder;
        //    }
        //    return innerhtml;
        //}
    }
}