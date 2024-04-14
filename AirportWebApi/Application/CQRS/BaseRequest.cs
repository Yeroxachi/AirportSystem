using Application.Response;
using Domain.Enums;
using Persistence.Context;

namespace Application.CQRS;

public abstract class BaseRequest
{
    protected readonly AirportContext _Context;

    protected BaseRequest(AirportContext context)
    {
        _Context = context;
    }

    protected BaseResponse Ok()
    {
        return new BaseResponse();
    }

    protected BaseResponse<TEntity> Ok<TEntity>(TEntity data) where TEntity : class
    {
        return new BaseResponse<TEntity>(data);
    }

    protected BaseResponse Created()
    {
        return new BaseResponse(ResponseCode.Created);
    }

    protected BaseResponse<TEntity> Created<TEntity>(TEntity data) where TEntity : class
    {
        return new BaseResponse<TEntity>(data, ResponseCode.Created);
    }

    protected BaseResponse UnAuthorized()
    {
        return new BaseResponse(ResponseCode.UnAuthorize);
    }

    protected BaseResponse<TEntity> UnAuthorized<TEntity>() where TEntity : class
    {
        return new BaseResponse<TEntity>(code: ResponseCode.UnAuthorize);
    }

    protected BaseResponse<TEntity> Forbid<TEntity>() where TEntity : class
    {
        return new BaseResponse<TEntity>(code: ResponseCode.Forbidden);
    }

    protected BaseResponse Forbid()
    {
        return new BaseResponse(ResponseCode.Forbidden);
    }
    
    protected BaseResponse NotFound(string message)
    {
        return new BaseResponse(ResponseCode.NotFound, message);
    }

    protected BaseResponse<TEntity> NotFound<TEntity>() where TEntity : class
    {
        return new BaseResponse<TEntity>(code: ResponseCode.NotFound);
    }

    protected BaseResponse BadRequest(string message)
    {
        return new BaseResponse(ResponseCode.BadRequest, errorMessage: message);
    }

    protected BaseResponse<TEntity> BadRequest<TEntity>(string message) where TEntity : class
    {
        return new BaseResponse<TEntity>(code: ResponseCode.BadRequest, errorMessage: message);
    }
    
    protected BaseResponse NoContent()
    {
        return new BaseResponse(ResponseCode.NoContent);
    }

    protected BaseResponse<TEntity> NoContent<TEntity>() where TEntity : class
    {
        return new BaseResponse<TEntity>(code: ResponseCode.NoContent);
    }
}