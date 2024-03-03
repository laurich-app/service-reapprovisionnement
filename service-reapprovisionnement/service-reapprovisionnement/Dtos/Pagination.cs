using Amazon.Runtime;

namespace Dtos;

public record Pagination <T> (List<T> data, Paginated pagination);