import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { map, Observable } from 'rxjs';
import { CreateProductRequest } from 'src/app/Model/Interfaces/products/request/CreateProductRequest';
import { EditProductRequest } from 'src/app/Model/Interfaces/products/request/EditProductRequest';
import { GetAllProductsResponse } from 'src/app/Model/Interfaces/products/response/GetAllProductsResponse';
import { ProductResponse } from 'src/app/Model/Interfaces/products/response/ProductResponse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private API_URL = environment.API_URL;

  private JWT_TOKEN = this.cookie.get('USER_INFO');

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.JWT_TOKEN}`,
    }),
  };

  constructor(private http: HttpClient, private cookie: CookieService) { }

  
  getAllProduct(pageSize: number, page: number): Observable<GetAllProductsResponse> {
    return this.http.get<GetAllProductsResponse>(
      `${this.API_URL}/Product/v1/lists-products?pageSize=${pageSize}&page=${page}`,
      this.httpOptions
    )

  }
  
  deleteProduct(product_id: string): Observable<ProductResponse> {
    return this.http.delete<ProductResponse>(
      `${this.API_URL}/Product/v1/remove-product/${product_id}`,
      this.httpOptions
    )
  }
  
  createProduct(product: CreateProductRequest): Observable<ProductResponse> {
    return this.http.post<ProductResponse>(
      `${this.API_URL}/Product/v1/created-product`, product,
      this.httpOptions
    )
  }

  editProduct(requestData: EditProductRequest, productNumber: string): Observable<ProductResponse> {
    return this.http.put<ProductResponse>(
      `${this.API_URL}/Product/v1/update-product/${productNumber}`,
      requestData, 
      this.httpOptions
    )
  }
}
