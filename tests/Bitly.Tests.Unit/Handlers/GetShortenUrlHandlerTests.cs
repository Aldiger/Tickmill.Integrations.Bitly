using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Tickmill.Integrations.Bitly.Core.Dto;
using Tickmill.Integrations.Bitly.Core.Integrations;
using Tickmill.Integrations.Bitly.Core.Integrations.Dto;
using Tickmill.Integrations.Bitly.Core.Options;
using Tickmill.Integrations.Bitly.Core.Queries;
using Tickmill.Integrations.Bitly.Core.Queries.Handlers;
using Convey.CQRS.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using Tickmill.Common.Types;
using Tickmill.Integrations.Bitly.Core.Exceptions;

using Xunit;

namespace Tickmill.Integrations.Bitly.Tests.Unit.Handlers;

[ExcludeFromCodeCoverage]
public class GetShortenUrlHandlerTests
{
    private Task<ShortenUrlDto> Act(GetShortenUrl query) => _handler.HandleAsync(query);

    [Fact]
    public async Task handler_should_return_result_given_valid_request_data()
    {
        //Arrange
        var query = GetQuery();

        _bitlyService.ShortenUrl(Arg.Any<BitlyShortenUrlRequestDto>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(new BitlyShortenUrlDto { Link = "test" }));
        
        //Act
        var result = await Act(query);
        
        //Assert
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task handler_should_fail_given_invalid_url()
        => ShortenUrlCustomException<UrlCannotBeInvalidException>(
            await Record.ExceptionAsync(() => Act(GetQuery(url:"test"))));


    [Fact]
    public async Task handler_should_fail_given_empty_url()
       => ShortenUrlCustomException<UrlCannotBeEmptyException>(
           await Record.ExceptionAsync(() => Act(GetQuery(url: string.Empty))));


    private static GetShortenUrl GetQuery(string url = "https://www.tutorialspoint.com/software_architecture_design/introduction.htm")
        => new(url);

    private static void ShortenUrlCustomException<T>(Exception exception, string message = null)
        where T : CustomException
    {
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<T>();
        var customException = exception as T;
        customException.ShouldNotBeNull();
        if (!string.IsNullOrWhiteSpace(message))
        {
            customException.Message.ShouldBe(message);
        }
    }

    #region Arrange

    private readonly IBitlyService _bitlyService;
    private readonly IQueryHandler<GetShortenUrl, ShortenUrlDto> _handler;
    private readonly ILogger<GetShortenUrlHandler> _logger;
    private readonly IOptions<BitlyOptions> _options;

    public GetShortenUrlHandlerTests()
    {
        _logger = Substitute.For<ILogger<GetShortenUrlHandler>>();
        _bitlyService = Substitute.For<IBitlyService>();
        _options= Substitute.For<IOptions<BitlyOptions>>();
        _options.Value.Returns(new BitlyOptions { ApiKey="",BaseApiUrl="",GroupId=""  });
        _handler = new GetShortenUrlHandler(_bitlyService,_options, _logger);
    }

    #endregion
}