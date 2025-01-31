import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { GetAllProductsDetailsResponse } from 'src/app/Model/Interfaces/products/response/GetAllProductsResponse';
import { ProductsService } from 'src/app/services/products/products.service';

@Component({
  selector: 'app-dashboard-home',
  templateUrl: './dashboard-home.component.html',
  styleUrls: [],
})
export class DashboardHomeComponent {

  public productsList: Array<GetAllProductsDetailsResponse> = [];
  page:number = 1;
  pageSize: number = 10;

  constructor(
    private productsService: ProductsService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.getProductsData();
  }
  getProductsData(): void {
    this.productsService
      .getAllProduct(this.pageSize, this.page)
      .subscribe({
        next: (response) => {

          this.productsList = response.data;

          debugger
          console.log('DADOS DOS PRODUTOS', this.productsList);

        }, error: (err) => {
          console.log(err);
          this.messageService.add({
            severity: 'error',
            summary: 'Erro',
            detail: 'Erro ao buscar produtos!',
            life: 3500,
          });
        },
      });
  }

}
