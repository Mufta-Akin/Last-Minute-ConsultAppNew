#pragma checksum "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\_Symptoms.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "58592ab61fea368e5cf6440b77c5a413f41ccefe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ailment__Symptoms), @"mvc.1.0.view", @"/Views/Ailment/_Symptoms.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"58592ab61fea368e5cf6440b77c5a413f41ccefe", @"/Views/Ailment/_Symptoms.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d0ab407c6397a65906242bd2484ead0e9e10801", @"/Views/_ViewImports.cshtml")]
    public class Views_Ailment__Symptoms : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<AilmentSymptom>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>Name</th>\n            <th>Severity</th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 11 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\_Symptoms.cshtml"
         foreach (var s in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>");
#nullable restore
#line 14 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\_Symptoms.cshtml"
           Write(s.Symptom.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 15 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\_Symptoms.cshtml"
           Write(s.Significance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n        </tr>\n");
#nullable restore
#line 17 "C:\Users\mufta\Desktop\WorkFolder\ConsultAppNew\Consultation.Web\Views\Ailment\_Symptoms.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<AilmentSymptom>> Html { get; private set; }
    }
}
#pragma warning restore 1591
