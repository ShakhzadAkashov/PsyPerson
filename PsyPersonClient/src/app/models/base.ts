export interface PagedResponse<T>{
    data: T[];
    total: number;
    loading?: boolean;
}

export interface PagedRequest{
    page: number;
    itemPerPage: number;
    [key: string]: any;
}

export class TableFilter{
    itemPerPage: number = 10;
    first: number = 0; 
}