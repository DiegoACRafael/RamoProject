export interface GetAllProductsResponse
{
    currentPage: number;
    totalPage: number;
    pageSize: number;
    totalCount: number;
    data : GetAllProductsDetailsResponse[];
}

export interface GetAllProductsDetailsResponse
{
    id: string;
    name:string;
    price : number;
    description : string;
}