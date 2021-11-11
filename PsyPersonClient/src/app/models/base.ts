export interface PagedResponse<T>{
    data: T[];
    total: number;
}

export interface PagedRequest{
    page: number;
    itemPerPage: number;
}