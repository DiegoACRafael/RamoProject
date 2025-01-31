import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DeleteProductAction } from 'src/app/Model/enum/products/DeleteProductAction';
import { ProductsEvents } from 'src/app/Model/enum/products/ProductsEvents';
import { EventAction } from 'src/app/Model/Interfaces/products/event/EventAction';
import { GetAllProductsDetailsResponse } from 'src/app/Model/Interfaces/products/response/GetAllProductsResponse';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: []
})
export class ProductsTableComponent {
  @Input() products: Array<GetAllProductsDetailsResponse> = []
  @Output() productEvent = new EventEmitter<EventAction>();
  @Output() deleteProductEvent = new EventEmitter<DeleteProductAction>();


  public productSelected!: GetAllProductsDetailsResponse;
  public addProductEvent = ProductsEvents.ADD_PRODUCT_EVENT;
  public editProductEvent = ProductsEvents.EDIT_PRODUCT_EVENT;


  handleProductEvent(action: string, id?: string): void {
    if (action && action !== '') {
      const productEventData = id && id !== '' ? { action, id } : { action };
      this.productEvent.emit(productEventData);
    }
  }

  handleDeleteProduct(product_id: string, productName: string): void{
    if (product_id !== '' && productName !== '') {
      this.deleteProductEvent.emit({
        product_id,
        productName,
      })
    }
  }
}
