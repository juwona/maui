using System;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Converters;
using Xunit;


namespace Microsoft.Maui.Controls.Core.UnitTests
{
	
	public class PointTests : BaseTestFixtureXUnit
	{
		[Fact]
		public void TestPointEquality()
		{
			Assert.True(new Point(0, 1) != new Point(1, 0));
			Assert.True(new Point(5, 5) == new Point(5, 5));
		}

		[Fact]
		public void TestPointDistance()
		{
			Assert.That(new Point(2, 2).Distance(new Point(5, 6)), Is.EqualTo(5).Within(0.001));
		}

		[Fact]
		public void TestPointMath()
		{
			var point = new Point(2, 3) + new Size(3, 2);
			Assert.Equal(new Point(5, 5), point);

			point = new Point(3, 4) - new Size(2, 3);
			Assert.Equal(new Point(1, 1), point);
		}

		[Fact]
		public void TestPointFromSize()
		{
			var point = new Point(new Size(10, 20));

			Assert.Equal(10, point.X);
			Assert.Equal(20, point.Y);
		}

		[Fact]
		public void TestPointOffset()
		{
			var point = new Point(2, 2);

			point = point.Offset(10, 20);

			Assert.Equal(new Point(12, 22), point);
		}

		[Fact]
		public void TestPointRound()
		{
			var point = new Point(2.4, 2.7);
			point = point.Round();

			Assert.Equal(Math.Round(2.4), point.X);
			Assert.Equal(Math.Round(2.7), point.Y);
		}

		[Fact]
		public void TestPointEmpty()
		{
			var point = new Point();

			Assert.True(point.IsEmpty);
		}

		[Fact]
		public void TestPointHashCode([Range(3, 6)] double x1, [Range(3, 6)] double y1, [Range(3, 6)] double x2,
									   [Range(3, 6)] double y2)
		{
			bool result = new Point(x1, y1).GetHashCode() == new Point(x2, y2).GetHashCode();
			if (x1 == x2 && y1 == y2)
				Assert.True(result);
			else
				Assert.False(result);
		}

		[Fact]
		public void TestSizeFromPoint()
		{
			var point = new Point(2, 4);
			var size = (Size)point;

			Assert.Equal(new Size(2, 4), size);
		}

		[Fact]
		public void TestPointEquals()
		{
			var point = new Point(2, 4);

			Assert.True(point.Equals(new Point(2, 4)));
			Assert.False(point.Equals(new Point(3, 4)));
			Assert.False(point.Equals("Point"));
		}

		[Fact]
		[InlineData(0, 0, ExpectedResult = "{X=0 Y=0}")]
		[InlineData(5, 2, ExpectedResult = "{X=5 Y=2}")]
		public string TestPointToString(double x, double y)
		{
			return new Point(x, y).ToString();
		}

		[Fact]
		public void TestPointTypeConverter()
		{
			var converter = new PointTypeConverter();
			Assert.True(converter.CanConvertFrom(typeof(string)));
			Assert.Equal(new Point(1, 2), converter.ConvertFromInvariantString("1,2"));
			Assert.Equal(new Point(1, 2), converter.ConvertFromInvariantString("1, 2"));
			Assert.Equal(new Point(1, 2), converter.ConvertFromInvariantString(" 1 , 2 "));
			Assert.Equal(new Point(1.1, 2), converter.ConvertFromInvariantString("1.1,2"));
			Assert.Throws<InvalidOperationException>(() => converter.ConvertFromInvariantString(""));
		}

	}
}
