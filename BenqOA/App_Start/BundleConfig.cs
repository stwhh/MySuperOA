using System.Web;
using System.Web.Optimization;

namespace BenqOA
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(

                        "~/Scripts/jquery.validate.js"));

            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            //通用css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/Style.css"));

            //common-js
            bundles.Add(new ScriptBundle("~/Scripts/common").Include("~/Scripts/common.js"));

            //bootStrap-css
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap/css/bootstrap.css"));

            //bootStrap-js
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/bootstrap.js"));

            //kendoUI-css
            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                        "~/Content/kendo/kendo.common.css",
                        "~/Content/kendo/kendo.default.css",
                        "~/Content/kendo/kendo.silver.css",
                        "~/Content/kendo/kendo.dataviz.css",
                        "~/Content/kendo/kendo.dataviz.default.css"));

            //kendoUI-js
            bundles.Add(new ScriptBundle("~/Scripts/kendo").Include(
                        "~/Scripts/kendo/kendo.all.js",
                        "~/Scripts/kendo/cultures/kendo.culture.zh-CN.js",
                        "~/Scripts/kendo/cultures/kendo.culture.zh.js",
                        "~/Scripts/kendo/messages/kendo.messages.zh-CN.js",
                        "~/Scripts/knockout-kendo.js"
                        ));

            //knockOut-js
            bundles.Add(new ScriptBundle("~/Scripts/knockout").Include(
                        "~/Scripts/jquery.tmpl.js",
                        "~/Scripts/knockout-3.3.0.js"));

            //ace-css
            bundles.Add(new StyleBundle("~/Content/ace").Include(
                    "~/Content/ace/css/bootstrap.css",
                    "~/Content/ace/css/bootstrap.extend.css",
                    "~/Content/ace/css/font-awesome.css",
                    "~/Content/ace/css/ace.css",
                    "~/Content/ace/css/ace-rtl.css",
                    "~/Content/ace/css/ace-skins.css"
                    ));

            //ace-js
            bundles.Add(new ScriptBundle("~/Scripts/ace").Include(
                     "~/Content/ace/js/bootstrap.js",
                     "~/Content/ace/js/bootstrap.extend.js",
                     "~/Content/ace/js/bootbox.js",
                     "~/Content/ace/js/typeahead-bs2.js",
                     "~/Content/ace/js/ace-extra.js",
                     "~/Content/ace/js/ace-elements.js",
                     "~/Content/ace/js/ace.js"
                     ));

            //jquery-validate
            bundles.Add(new ScriptBundle("~/Scripts/jqueryvalidate").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.messages_zh.js",
                "~/Scripts/jquery.validate.expand.js"));

            //plupload -css
            bundles.Add(new StyleBundle("~/Content/plupload").Include(
                "~/Content/plupload/css/jquery.plupload.queue.css"
                ));

            //plupload -js
            bundles.Add(new ScriptBundle("~/Scripts/plupload").Include(
                "~/Scripts/plupload/plupload.full.js",
                "~/Scripts/plupload/jquery.plupload.queue.js",
                "~/Scripts/plupload/zh_CN.js"));
        }

    }
}