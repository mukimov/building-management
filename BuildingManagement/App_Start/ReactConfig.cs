using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BuildingManagement.ReactConfig), "Configure")]

namespace BuildingManagement {
	public static class ReactConfig {
		public static void Configure() {
			// If you want to use server-side rendering of React components, 
			// add all the necessary JavaScript files here. This includes 
			// your components as well as all of their dependencies.
			// See http://reactjs.net/ for more information. Example:
			ReactSiteConfiguration.Configuration
				.AddScript("~/Scripts/moment.min.js")
				.AddScript("~/Scripts/modal.jsx")
				.AddScript("~/Scripts/tenant-button.jsx")
				.AddScript("~/Scripts/tenants.jsx")
				.SetJsonSerializerSettings(new JsonSerializerSettings {
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					ContractResolver = new CamelCasePropertyNamesContractResolver(),
				});
			//	.AddScript("~/Scripts/Second.jsx");

			// If you use an external build too (for example, Babel, Webpack,
			// Browserify or Gulp), you can improve performance by disabling 
			// ReactJS.NET's version of Babel and loading the pre-transpiled 
			// scripts. Example:
			//ReactSiteConfiguration.Configuration
			//	.SetLoadBabel(false)
			//	.AddScriptWithoutTransform("~/Scripts/bundle.server.js")
		}
	}
}