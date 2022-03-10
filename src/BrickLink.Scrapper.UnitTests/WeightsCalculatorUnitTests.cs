using BrickLink.Scraper;
using FluentAssertions;
using Xunit;

namespace BrickLink.Scrapper.UnitTests;

public class WeightsCalculatorUnitTests
{
    [Theory]
    [InlineData(3, 0)]
    [InlineData(2.5, 0)]
    [InlineData(1.75, 55)]
    [InlineData(1, 85)]
    [InlineData(0.5, 100)]
    [InlineData(0, 100)]
    public void CalculatePricePointScore(double affordability, double expected)
    {
        // Arrange
        var weightsCalculator = new WeightsCalculator();
        
        // Act
        var score = weightsCalculator.CalculatePricePointScore(affordability);
        
        // Assert
        score.Should().BeInRange(expected - 1, expected + 2);
    }
}