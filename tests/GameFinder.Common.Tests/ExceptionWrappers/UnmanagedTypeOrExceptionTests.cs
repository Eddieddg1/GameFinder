namespace GameFinder.Common.Tests;

public class UnmanagedTypeOrExceptionTests
{
    private const int TestValue = 1;
    private static UnmanagedTypeOrException<int> CreateWithValue() => new(value: TestValue);

    private static (UnmanagedTypeOrException<int>, Exception) CreateWithException()
    {
        var exception = new NotSupportedException("Testing");
        return (new UnmanagedTypeOrException<int>(exception), exception);
    }

    [Fact]
    public void Test_GetValue_WithValue()
    {
        var value = CreateWithValue();

        var act = () => value.GetValue();
        act.Should().NotThrow<InvalidOperationException>();

        var res = act();
        res.Should().Be(TestValue);
    }

    [Fact]
    public void Test_GetValue_WithException()
    {
        var (value, _) = CreateWithException();

        var act = () => value.GetValue();
        act
            .Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("This instance doesn't contain a value but an exception: \"System.NotSupportedException: Testing\"");
    }

    [Fact]
    public void Test_GetException_WithValue()
    {
        var value = CreateWithValue();

        var act = () => value.GetException();
        act
            .Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("This instance doesn't contain an exception but a value: \"1\"");
    }

    [Fact]
    public void Test_GetException_WithException()
    {
        var (value, exception) = CreateWithException();

        var act = () => value.GetException();
        act.Should().NotThrow<InvalidOperationException>();

        var res = act();
        res.Should().Be(exception);
    }

    [Fact]
    public void Test_ThrowException_WithValue()
    {
        var value = CreateWithValue();

        var act = () => value.ThrowException();
        act
            .Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("This instance doesn't contain an exception but a value: \"1\"");
    }

    [Fact]
    public void Test_ThrowException_WithException()
    {
        var (value, exception) = CreateWithException();

        var act = () => value.ThrowException();
        act.Should().ThrowExactly<NotSupportedException>().And.Should().Be(exception);
    }

    [Fact]
    public void HasValue_WithValue()
    {
        var value = CreateWithValue();

        var res = value.HasValue();
        res.Should().BeTrue();
    }

    [Fact]
    public void HasValue_WithException()
    {
        var (value, _) = CreateWithException();

        var res = value.HasValue();
        res.Should().BeFalse();
    }
}
