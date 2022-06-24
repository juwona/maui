using Microsoft.Maui.Graphics;
using Xunit;

namespace Microsoft.Maui.Controls.Core.UnitTests
{
	
	public class BrushUnitTests : BaseTestFixtureXUnit
	{
		BrushTypeConverter _converter;

		
		public void SetUp()
		{
			_converter = new BrushTypeConverter();
		}

		[Fact]
		[InlineData("rgb(6, 201, 198)")]
		[InlineData("rgba(6, 201, 188, 0.2)")]
		[InlineData("hsl(6, 20%, 45%)")]
		[InlineData("hsla(6, 20%, 45%,0.75)")]
		[InlineData("rgb(100%, 32%, 64%)")]
		[InlineData("rgba(100%, 32%, 64%,0.27)")]
		public void TestBrushTypeConverterWithColorDefinition(string colorDefinition)
		{
			Assert.True(_converter.CanConvertFrom(typeof(string)));
			Assert.NotNull(_converter.ConvertFromInvariantString(colorDefinition));
		}

		[Fact]
		[InlineData("#ff00ff")]
		[InlineData("#00FF33")]
		[InlineData("#00FFff 40%")]
		public void TestBrushTypeConverterWithColorHex(string colorHex)
		{
			Assert.True(_converter.CanConvertFrom(typeof(string)));
			Assert.NotNull(_converter.ConvertFromInvariantString(colorHex));
		}

		[Fact]
		[InlineData("linear-gradient(90deg, rgb(255, 0, 0),rgb(255, 153, 51))")]
		[InlineData("radial-gradient(circle, rgb(255, 0, 0) 25%, rgb(0, 255, 0) 50%, rgb(0, 0, 255) 75%)")]
		public void TestBrushTypeConverterWithBrush(string brush)
		{
			Assert.True(_converter.CanConvertFrom(typeof(string)));
			Assert.NotNull(_converter.ConvertFromInvariantString(brush));
		}

		[Fact]
		public void TestBindingContextPropagation()
		{
			var context = new object();
			var linearGradientBrush = new LinearGradientBrush();

			var firstStop = new GradientStop { Offset = 0.1f, Color = Colors.Red };
			var secondStop = new GradientStop { Offset = 1.0f, Color = Colors.Blue };

			linearGradientBrush.GradientStops.Add(firstStop);
			linearGradientBrush.GradientStops.Add(secondStop);

			linearGradientBrush.BindingContext = context;

			Assert.Same(context, firstStop.BindingContext);
			Assert.Same(context, secondStop.BindingContext);
		}

		[Fact]
		public void TestBrushParent()
		{
			var context = new object();

			var parent = new Grid
			{
				BindingContext = context
			};

			var linearGradientBrush = new LinearGradientBrush();

			var firstStop = new GradientStop { Offset = 0.1f, Color = Colors.Red };
			var secondStop = new GradientStop { Offset = 1.0f, Color = Colors.Blue };

			linearGradientBrush.GradientStops.Add(firstStop);
			linearGradientBrush.GradientStops.Add(secondStop);

			parent.Background = linearGradientBrush;

			Assert.Same(parent, parent.Background.Parent);
			Assert.Same(context, parent.Background.BindingContext);
		}
	}
}