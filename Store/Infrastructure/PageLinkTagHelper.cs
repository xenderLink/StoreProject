using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Store.Models.ViewModels;

///<sumary>
///Пагинация
///</sumary>

namespace Store.Infrasctructure;

[HtmlTargetElement ("div", Attributes = "page-model")]
public class PageLinkTagHelper : TagHelper
{
    private IUrlHelperFactory urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory helperFactory)
    {
        urlHelperFactory = helperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext? ViewContext {get;set;}
    public PagingInfo? PageModel {get; set;}
    public string? PageAction {get; set;}
    public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

    public bool PageClassesEnabled {get; set;} = false;
    public string PageClass {get; set;} = String.Empty;
    public string PageClassNormal {get; set;} = String.Empty;
    public string PageClassSelected {get; set;} = String.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if(ViewContext != null && PageModel != null)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder div = new TagBuilder("div");
            
            for(int i = 1; i<=PageModel.TotalPages(); i++)
            {
                TagBuilder anchor = new TagBuilder("a");
                PageUrlValues["productPage"] = i;
                
                anchor.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                
                if(PageClassesEnabled)
                {
                    anchor.AddCssClass(PageClass);
                    anchor.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                anchor.InnerHtml.Append(i.ToString());
                div.InnerHtml.AppendHtml(anchor);
            }

            output.Content.AppendHtml(div.InnerHtml);
        }
    }
}