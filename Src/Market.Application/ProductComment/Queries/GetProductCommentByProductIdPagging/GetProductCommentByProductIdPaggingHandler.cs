using Market.Application.Configurations.Queries;
using Market.Application.ProductComment.Queries.AggregateDto;
using Market.Domain.ProductComments;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.ProductComment.Queries.GetProductCommentByProductIdPagging;
public class GetProductCommentByProductIdPaggingHandler
    : IQueryHandler<GetProductCommentByProductIdPaggingQuery, List<ProductCommentsDto>>
{
    private readonly IProductCommentRepository productCommentRepository;
    private readonly IUserRepository userRepository;
    private readonly ILogger<GetProductCommentByProductIdPaggingHandler> logger;

    public GetProductCommentByProductIdPaggingHandler(
        IProductCommentRepository productCommentRepository, IUserRepository userRepository,
        ILogger<GetProductCommentByProductIdPaggingHandler> logger)
    {
        this.productCommentRepository = productCommentRepository;
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public async Task<List<ProductCommentsDto>> Handle(GetProductCommentByProductIdPaggingQuery request, CancellationToken cancellationToken)
    {
        int Page = request.Page;
        int PageSize = request.PageSize;

        if (Page < 0) Page = 0;
        if (PageSize < 0 || PageSize > 20) Page = 10;

        var allProductCommentByProductId =
            await productCommentRepository.GetCommentsByProductIdAsync(request.ProductId);

        List<ProductCommentAggregate> productCommentPagging =
            allProductCommentByProductId.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

        List<ProductCommentsDto> productCommentsDtoPagging = new();
        productCommentPagging.ForEach(async p => {
            var user = await userRepository.GetUserByUserIdAsync(p.UserCommentId);
            ProductCommentsDto productCommentDto = ProductCommentsDto.ConverProductCommentsToDto(p, user);
            productCommentsDtoPagging.Add(productCommentDto);
        });

        return productCommentsDtoPagging;
    }
}
