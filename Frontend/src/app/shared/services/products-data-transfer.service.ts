import { Injectable } from '@angular/core';
import { BehaviorSubject, map, take } from 'rxjs';
import { GetAllProductsDetailsResponse, GetAllProductsResponse } from 'src/app/Model/Interfaces/products/response/GetAllProductsResponse';

@Injectable({
  providedIn: 'root'
})
export class ProductsDataTransferService {

  public productsDataEmitter$ =
    new BehaviorSubject<Array<GetAllProductsDetailsResponse> | null>(null);

  public productsDatas = Array<GetAllProductsDetailsResponse>();


  setProductsDatas(products: Array<GetAllProductsDetailsResponse>): void {
    if (products) {
      this.productsDataEmitter$.next(products);
      this.getProductsDatas();
    }
  }
  getProductsDatas() {
    this.productsDataEmitter$
      .pipe(take(1))
      .subscribe({
        next: (response) => {
          if (response) {
            this.productsDatas = response;
          }
        },
      });

    return this.productsDatas;
  }

}
