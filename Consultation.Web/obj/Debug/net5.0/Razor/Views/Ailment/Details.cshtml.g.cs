#pragma checksum "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dbb890ea155c81ae4063e744584d7435a418e6bf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ailment_Details), @"mvc.1.0.view", @"/Views/Ailment/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\_ViewImports.cshtml"
using Consultation.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\_ViewImports.cshtml"
using Consultation.Web.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\_ViewImports.cshtml"
using Consultation.Data.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbb890ea155c81ae4063e744584d7435a418e6bf", @"/Views/Ailment/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d0ab407c6397a65906242bd2484ead0e9e10801", @"/Views/_ViewImports.cshtml")]
    public class Views_Ailment_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AilmentConditionViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Symptoms", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Patient", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""mt-4 mb-4"">
    <a class=""btn btn-info mr-4"">
        <i class=""bi bi mr-1""></i>Ailment and Condition Details
    </a>
</div>


<div class=""card shadow rounded p-3"">

    <div class=""row pl-3"">
        <!-- Details -->
        <div class=""col-8 pt-3"">
            <dl class=""row"">
                <dt class=""col-5"">Patient</dt>
                <dd class=""col-7"">");
#nullable restore
#line 18 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
                             Write(Model.PatientName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n\n                <dt class=\"col-5\">Issue</dt>\n                <dd class=\"col-7\">");
#nullable restore
#line 21 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
                             Write(Model.Issue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n\n                <dt class=\"col-5\">CreatedOn</dt>\n                <dd class=\"col-7\">");
#nullable restore
#line 24 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
                             Write(Model.CreatedOn);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n\n                <dt class=\"col-5\">Active</dt>\n                <dd class=\"col-7\">");
#nullable restore
#line 27 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
                             Write(Model.Active);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n\n            </dl>\n        </div>\n    </div>\n\n    <!-- display symptoms for this ailment -->\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "dbb890ea155c81ae4063e744584d7435a418e6bf6571", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
#nullable restore
#line 34 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.Symptoms;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n    <h4>Possible Conditions</h4>\n");
#nullable restore
#line 37 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
     foreach (var c in Model.PossibleConditions)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<p>");
#nullable restore
#line 39 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
Write(c.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>");
#nullable restore
#line 39 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\Details.cshtml"
                     }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\n<div class=\"mt-2\">\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dbb890ea155c81ae4063e744584d7435a418e6bf8955", async() => {
                WriteLiteral("Patient Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AilmentConditionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
