export interface ProductResponse {
    message: string;
    data: ProductResponseDetail;
}
export interface ProductResponseDetail {
    id: string;
    name: string;
    description: string;
    price: number;
}