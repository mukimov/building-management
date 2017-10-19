using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagement;
using BuildingManagement.Controllers;

namespace BuildingManagement.Tests.Controllers
{
	[TestClass]
	public class BuildingsControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			BuildingsController controller = new BuildingsController();

			// Act
			ViewResult result = controller.Index("Name", false) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
