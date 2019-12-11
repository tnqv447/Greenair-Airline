#pragma checksum "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ee57fdd58c64b6f8081609fd41aef9763069e37"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Presentation.Pages.Pages_Flight), @"mvc.1.0.razor-page", @"/Pages/Flight.cshtml")]
namespace Presentation.Pages
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
#line 1 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\_ViewImports.cshtml"
using Presentation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
using Presentation.Helpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ee57fdd58c64b6f8081609fd41aef9763069e37", @"/Pages/Flight.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d76d778d4c858045a827ae28cbb9bc28e1d5c23", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Flight : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("font-weight:600"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
     if(SessionHelper.GetObjectFromJson<Dictionary<string,object>>(HttpContext.Session,"FlightSearch") == null)
		{
			Response.Redirect("Index");
		}
	else
	{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"		<div style=""background-image: -webkit-linear-gradient(315deg,#f58300 0,#f59300 50%,#f6a100 50%,#f58300 100%);
		background-image: linear-gradient(135deg,#f58300 0,#f59300 50%,#f6a100 50%,#f58300 100%); width:100%;height:450px"">
			<div class=""overlay""></div>
			<div class=""container"">
				<div class=""row no-gutters slider-text js-fullheight align-items-center "" data-scrollax-parent=""true"">
					<div style=""width:100%;padding-left: 250px"">
						<h1 class=""mb-5"">Flight start here</h1>		
					</div>
				</div>
			</div>
		</div>
		<section class=""ftco-section bg-light"">

				<div class=""container flight-go"">
					<div class=""row"">
						<div class=""col-sm-12"" style=""text-align:center;padding-left: .9375rem;padding-right: .9375rem;"">
							<h2 style=""font-weight:600"">");
#nullable restore
#line 29 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                                   Write(ViewData["from_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n\t\t\t\t\t\t\t\tto\r\n\t\t\t\t\t\t\t\t");
#nullable restore
#line 31 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                           Write(ViewData["where_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t</h2>\r\n\t\t\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1ee57fdd58c64b6f8081609fd41aef9763069e375843", async() => {
                WriteLiteral("\r\n\t\t\t\t\t\t\t\tChange flight search\r\n\t\t\t\t\t\t\t\t<span class=\"ion-ios-color-wand\"></span>\r\n\t\t\t\t\t\t\t");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"col-sm-8\">\r\n\t\t\t\t\t\t\t<div");
            BeginWriteAttribute("class", " class=\"", 1382, "\"", 1390, 0);
            EndWriteAttribute();
            WriteLiteral(@">
								<div class=""section-heading"">
									<h3 class=""section-heading__title"" style=""font-weight:600"">
										Flight go
									</h3>
									<span class=""section-heading__icon ion-ios-plane""></span>
									<div class=""section-heading__content"">
										<span class=""section-heading__route"">
											");
#nullable restore
#line 47 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                       Write(ViewData["from_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t\t\t\t\tto\r\n\t\t\t\t\t\t\t\t\t\t\t");
#nullable restore
#line 49 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                       Write(ViewData["where_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t\t\t<span class=\"section-heading__content-connector\">\r\n\t\t\t\t\t\t\t\t\t\t\t-\r\n\t\t\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t\t\t<span class=\"section-heading__date\">\r\n\t\t\t\t\t\t\t\t\t\t\t");
#nullable restore
#line 55 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                       Write(ViewData["depDate"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class=\"row\">\r\n\t\t\t\t\t\t<div class=\"col-sm-3\">\r\n\t\t\t\t\t\t\t<label for=\"day-go\">Check in</label>\r\n\t\t\t\t\t\t\t<input type=\"text\"");
            BeginWriteAttribute("value", "  value=\'", 2204, "\'", 2240, 1);
#nullable restore
#line 65 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
WriteAttributeValue("", 2213, ViewData["value_dep_date"], 2213, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"check-in\">\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<p>\r\n\t\t\t\t\t\t");
#nullable restore
#line 69 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                   Write(Model.Msg);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t</p>\r\n");
#nullable restore
#line 72 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                             if(Model.ListFlights_1.Any())
							{
								

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                 foreach(var item in Model.ListFlights_1)
								{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"									<div class=""row"" style=""margin-top: 20px"">
										<div class=""col-sm-12"">
											<div class=""flight-card-wrapper"">
												<div class=""flight-card"">
													<div class=""flight-card-itinerary"">
														<div class=""itinerary-info"">
															<div class=""itinerary-info__time"">
																5:30
															</div>
															<div class=""itinerary-info__airport""> 
																	");
#nullable restore
#line 86 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                                               Write(ViewData["from"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
																	- Origin
															</div>
														</div>
														<div class=""itinerary-info__icons"">
															<span></span>
														</div>
														<div class=""itinerary-info"">
															<div class=""itinerary-info__time "">
																7:40
															</div>
															<div class=""itinerary-info__airport"">
																");
#nullable restore
#line 98 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                                           Write(ViewData["where"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" 
																- Destination
															</div>
														</div>
													</div>
													<div class=""flight-card-details-price"">
														<div class=""flight-card-details"">
															<div style=""float:left;"">
																<span class=""flight-card-details__duration "">Blasdasdasdasasdsad</span>
																<div class=""flight-card-details__more"">
																	<span class=""info-show"" >
																		Chi tiết
																	</span>
																	<span class=""info-hide"">
																		Ẩn
																	</span>
																</div>
															</div>
															
															<div class=""price-select"" style=""float:right"">
																<div class=""price-select__price  text-right"">
																	<div class=""starter-price-wrapper"">
																		<div class=""pricepoint-wrapper pricepoint-wrapper--orange"">
																			<div class=""pricepoint"">
																				<span class=""pricepoint__middle"">
																					");
            WriteLiteral(@"890,000
																					<span class=""pricepoint__decimal""></span>
																				</span>
																				<span class=""pricepoint__symbol"">
																					đ
																				</span>
																			</div>
																		</div>
																	</div>
																</div>
																<div class=""price-select__button"">
																	<div class=""button-toggle js-button-toggle"">
																		<span>
																			Chọn
																		</span>
																	</div>
																</div>
															</div>
														</div>
													</div>
												</div>
												<div class="" qa-flight-card-details"">
													<div class=""fare__details fare__details--leg-0 "">
														<span class=""carrier""></span>
														<div class=""flight-info"">
															<div class=""flight-info__row  flight-info__flightNubmer"">
																<div class=""flight-info__row__label medium-3""> 
																	Chuyến bay 1:
												");
            WriteLiteral(@"				</div>
																<div class=""medium-9"">
																	<p></p>
																</div>
															</div>
															<div class=""flight-info__row"">
																<div class=""flight-info__row__label medium-3""> 
																	Khởi hành:
																</div>
																<div class=""medium-9"">
																	<p></p>
																</div>
															</div>
															<div class=""flight-info__row"">
																<div class=""flight-info__row__label medium-3""> 
																	Đến:
																</div>
																<div class=""medium-9"">
																	<p></p>
																</div>
															</div>
															<div class=""flight-info__row"">
																<div class=""flight-info__row__label medium-3""> 
																</div>
																<div class=""flight-info__more medium-9"">
																	<div class=""flight-info__more__item"">
																		Thời gian:
																		<p>2hrs 10mins</p>
																	</div>
						");
            WriteLiteral(@"											<div class=""flight-info__more__item"">
																		Máy bay:
																		<p>2hrs 10mins</p>
																	</div>
																	<div class=""flight-info__more__item"">
																		Hãng khai thác:
																		<p>2hrs 10mins</p>
																	</div>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
");
#nullable restore
#line 196 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
								}

#line default
#line hidden
#nullable disable
#nullable restore
#line 196 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                 
							}
							else
							{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"								<div class=""row"">
									<div class=""col-sm-12"" style=""text-align:center;padding-left: .9375rem;padding-right: .9375rem;"">
											<h2 class=""section-heading__route"">
											No results
											</h2>
									</div>
								</div>
");
#nullable restore
#line 207 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
							}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t</div>\r\n");
#nullable restore
#line 211 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                 if(Model.CheckType == "round")
				{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t<div class=\"container flight-back\">\r\n\t\t\t\t\t\t<div class=\"row\">\r\n\t\t\t\t\t\t\t<div class=\"col-sm-12\" style=\"text-align:center;padding-left: .9375rem;padding-right: .9375rem;\">\r\n\t\t\t\t\t\t\t\t<h2 style=\"font-weight:600\">");
#nullable restore
#line 216 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                                       Write(ViewData["where_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n\t\t\t\t\t\t\t\t\tto\r\n\t\t\t\t\t\t\t\t\t");
#nullable restore
#line 218 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                               Write(ViewData["from_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t\t</h2>\r\n\t\t\t\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1ee57fdd58c64b6f8081609fd41aef9763069e3717462", async() => {
                WriteLiteral("\r\n\t\t\t\t\t\t\t\t\tChange flight search\r\n\t\t\t\t\t\t\t\t\t<span class=\"ion-ios-color-wand\"></span>\r\n\t\t\t\t\t\t\t\t");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t<div class=\"col-sm-8\">\r\n\t\t\t\t\t\t\t\t<div");
            BeginWriteAttribute("class", " class=\"", 7726, "\"", 7734, 0);
            EndWriteAttribute();
            WriteLiteral(@">
									<div class=""section-heading"">
										<h3 class=""section-heading__title"" style=""font-weight:600"">
											Flight back
										</h3>
										<span class=""section-heading__icon ion-ios-plane""></span>
										<div class=""section-heading__content"">
											<span class=""section-heading__route"">
												");
#nullable restore
#line 234 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                           Write(ViewData["where_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t\t\t\t\t\tto\r\n\t\t\t\t\t\t\t\t\t\t\t\t");
#nullable restore
#line 236 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                           Write(ViewData["from_city"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t\t\t\t<span class=\"section-heading__content-connector\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t-\r\n\t\t\t\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t\t\t\t<span class=\"section-heading__date\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t");
#nullable restore
#line 242 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                           Write(ViewData["arrDate"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"row\">\r\n\t\t\t\t\t\t\t<div class=\"col-sm-3\">\r\n\t\t\t\t\t\t\t\t<label for=\"day-go\">Check out</label>\r\n\t\t\t\t\t\t\t\t<input type=\"text\"");
            BeginWriteAttribute("value", "  value=\'", 8577, "\'", 8613, 1);
#nullable restore
#line 252 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
WriteAttributeValue("", 8586, ViewData["value_arr_date"], 8586, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"check-out\">\r\n\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t</div>\r\n");
#nullable restore
#line 255 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                         if(Model.ListFlights_2.Any())
						{
							

#line default
#line hidden
#nullable disable
#nullable restore
#line 257 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                             foreach(var item in Model.ListFlights_2)
							{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"								<div class=""row"" style=""margin-top: 20px"">
									<div class=""col-sm-12"">
										<div class=""flight-card-wrapper"">
											<div class=""flight-card"">
												<div class=""flight-card-itinerary"">
													<div class=""itinerary-info"">
														<div class=""itinerary-info__time"">
															5:30
														</div>
														<div class=""itinerary-info__airport""> 
																");
#nullable restore
#line 269 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                                           Write(ViewData["where"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
																- Origin
														</div>
													</div>
													<div class=""itinerary-info__icons"">
														<span></span>
													</div>
													<div class=""itinerary-info"">
														<div class=""itinerary-info__time "">
															7:40
														</div>
														<div class=""itinerary-info__airport"">
															");
#nullable restore
#line 281 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                                                       Write(ViewData["from"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" 
															- Destination
														</div>
													</div>
												</div>
												<div class=""flight-card-details-price"">
													<div class=""flight-card-details"">
														<div style=""float:left;"">
															<span class=""flight-card-details__duration "">Blasdasdasdasasdsad</span>
															<div class=""flight-card-details__more"">
																<span class=""info-show"" >
																	Chi tiết
																</span>
																<span class=""info-hide"">
																	Ẩn
																</span>
															</div>
														</div>
														
														<div class=""price-select"" style=""float:right"">
															<div class=""price-select__price  text-right"">
																<div class=""starter-price-wrapper"">
																	<div class=""pricepoint-wrapper pricepoint-wrapper--orange"">
																		<div class=""pricepoint"">
																			<span class=""pricepoint__middle"">
																				890,000
																");
            WriteLiteral(@"				<span class=""pricepoint__decimal""></span>
																			</span>
																			<span class=""pricepoint__symbol"">
																				đ
																			</span>
																		</div>
																	</div>
																</div>
															</div>
															<div class=""price-select__button"">
																<div class=""button-toggle js-button-toggle"">
																	<span>
																		Chọn
																	</span>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
											<div class="" qa-flight-card-details"">
												<div class=""fare__details fare__details--leg-0 "">
													<span class=""carrier""></span>
													<div class=""flight-info"">
														<div class=""flight-info__row  flight-info__flightNubmer"">
															<div class=""flight-info__row__label medium-3""> 
																Chuyến bay 1:
															</div>
															<div class=""medium-9"">
		");
            WriteLiteral(@"														<p></p>
															</div>
														</div>
														<div class=""flight-info__row"">
															<div class=""flight-info__row__label medium-3""> 
																Khởi hành:
															</div>
															<div class=""medium-9"">
																<p></p>
															</div>
														</div>
														<div class=""flight-info__row"">
															<div class=""flight-info__row__label medium-3""> 
																Đến:
															</div>
															<div class=""medium-9"">
																<p></p>
															</div>
														</div>
														<div class=""flight-info__row"">
															<div class=""flight-info__row__label medium-3""> 
															</div>
															<div class=""flight-info__more medium-9"">
																<div class=""flight-info__more__item"">
																	Thời gian:
																	<p>2hrs 10mins</p>
																</div>
																<div class=""flight-info__more__item"">
																	Máy bay:
					");
            WriteLiteral(@"												<p>2hrs 10mins</p>
																</div>
																<div class=""flight-info__more__item"">
																	Hãng khai thác:
																	<p>2hrs 10mins</p>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
");
#nullable restore
#line 379 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
							}

#line default
#line hidden
#nullable disable
#nullable restore
#line 379 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
                             
						}
						else
						{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t\t<div class=\"row\">\r\n\t\t\t\t\t\t\t\t<div class=\"col-sm-12\" style=\"text-align:center;padding-left: .9375rem;padding-right: .9375rem;\">\r\n\t\t\t\t\t\t\t\t\t\t<h2 class=\"section-heading__route\">\r\n\t\t\t\t\t\t\t\t\t\tNo results\r\n\t\t\t\t\t\t\t\t\t\t</h2>\r\n\t\t\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t\t</div>\r\n");
#nullable restore
#line 390 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
						}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t\r\n\t\t\t\t\t\t\r\n\t\t\t\t\t</div>\r\n");
#nullable restore
#line 394 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
				}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"				<!-- }
			} -->
		</section>
		<script>
			$(document).ready(function()
			{ 
				$(document).on(""focus"",""#check-in"",function(){
					$(this).datepicker({
					dateFormat: ""dd/mm/yy"",
					minDate: new Date(),
					'autoclose': true
					});
				});
				
				$(document).on(""focus"",""#check-out"",function(){
					$(this).datepicker({
						dateFormat: ""dd/mm/yy"",
						minDate: $(""#check-in"").val(),
					});
				});
				$(document).on(""change"",""#check-in"",function()
				{
					var type_date = ""check_in"";
					var choose = $(""#check-in"").val();
					var arrdate = $(""#check-out"").val();
					var check = """";
					if(choose > arrdate)
					{
						$(""#check-out"").val(choose);
						check = ""true"";
					}
					$(""#check-out"").datepicker(""option"",""minDate"",$(""#check-in"").val());
					$.ajax({
						type: ""GET"",
						url: ""/Flight?handler=NewDate"",
						dataType: 'json',
						contentType: 'application/json; charset=utf-8',
						data: { 
							choose: choose,
							type_d");
            WriteLiteral(@"ate: type_date,
							check: check
						},
						success:function(response)
						{
							$("".flight-go"").load(""/Flight"" + "" .flight-go"");

							$("".flight-back"").load(""/Flight"" + "" .flight-back"");
						}
					});
					
				});
				$(document).on(""change"",""#check-out"",function()
				{
					var type_date = ""check_out"";
					var choose = $(""#check-out"").val();
					$.ajax({
						type: ""GET"",
						url: ""/Flight?handler=NewDate"",
						dataType: 'json',
						contentType: 'application/json; charset=utf-8',
						data: { 
							choose: choose,
							type_date: type_date
						},
						success:function(response)
						{
							$("".flight-back"").load(""/Flight"" + "" .flight-back"");
						}
					});
					
				});
			});
		</script>
");
#nullable restore
#line 468 "C:\Users\Administrator\Desktop\Greenair\presentation\Pages\Flight.cshtml"
	}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Presentation.Pages.FlightModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Presentation.Pages.FlightModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Presentation.Pages.FlightModel>)PageContext?.ViewData;
        public Presentation.Pages.FlightModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591