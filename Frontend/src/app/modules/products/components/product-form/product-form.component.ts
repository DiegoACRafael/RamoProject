import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ProductsEvents } from 'src/app/Model/enum/products/ProductsEvents';
import { EventAction } from 'src/app/Model/Interfaces/products/event/EventAction';
import { CreateProductRequest } from 'src/app/Model/Interfaces/products/request/CreateProductRequest';
import { EditProductRequest } from 'src/app/Model/Interfaces/products/request/EditProductRequest';
import { GetAllProductsDetailsResponse, GetAllProductsResponse } from 'src/app/Model/Interfaces/products/response/GetAllProductsResponse';
import { ProductsService } from 'src/app/services/products/products.service';
import { ProductsDataTransferService } from 'src/app/shared/services/products-data-transfer.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: []
})
export class ProductFormComponent implements OnInit, OnDestroy {
  private readonly destroy$: Subject<void> = new Subject();
  public productAction!: {
    event: EventAction;
    productsDatas: Array<GetAllProductsDetailsResponse>;
  }
  public productSelectedDatas!: GetAllProductsDetailsResponse;
  public productsDatas: Array<GetAllProductsDetailsResponse> = [];

  public addProductForm = this.formBuilder.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0, Validators.required]
  });

  public editProductForm = this.formBuilder.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0, Validators.required]
  });

  public addProductAction = ProductsEvents.ADD_PRODUCT_EVENT;
  public editProductAction = ProductsEvents.EDIT_PRODUCT_EVENT;


  constructor(
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private router: Router,
    private productService: ProductsService,
    private ref: DynamicDialogConfig,
    private productsDtService: ProductsDataTransferService
  ) { }

  ngOnInit(): void {
    this.productAction = this.ref.data;
    if (
      this.productAction?.event?.action === this.editProductAction
    ) {
      this.getProductSelectedDatas(this.productAction?.event?.id as string);
    }
  }

  handleSubmitAddProduct(): void {
    if (this.addProductForm?.value && this.addProductForm?.valid) {
      const createProductRequest: CreateProductRequest = {
        name: this.addProductForm.value.name as string,
        description: this.addProductForm.value.description as string,
        price: Number(this.addProductForm.value.price)
      }
      this.productService.createProduct(createProductRequest)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (response) => {
            if (response)
              this.messageService.add({
                severity: 'success',
                summary: 'Sucesso',
                detail: 'Produto criado com sucesso!',
                life: 3000,
              });
          },
          error: (err) => {
            console.log(err);
            this.messageService.add({
              severity: 'error',
              summary: 'Erro',
              detail: 'Erro ao criar produto!',
              life: 3000,
            });
          },
        });
    }
    this.addProductForm.reset();
  }

  handleSubmitEditProduct(): void {

    if (
      this.editProductForm.value &&
      this.editProductForm.valid &&
      this.productAction.event.id
    ) { 
      const requestEditProduct: EditProductRequest={
        name: this.editProductForm?.value?.name as string,
        description: this.editProductForm?.value?.description as string,
        price: Number(this.editProductForm.value.price)
      }

      this.productService.editProduct(requestEditProduct, this.productAction.event.id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          if (response)
            this.messageService.add({
              severity: 'success',
              summary: 'Sucesso',
              detail: 'Produto editado com sucesso!',
              life: 3000,
            });
            this.editProductForm.reset();
        },
        error: (err) => {
          console.log(err);
          this.messageService.add({
            severity: 'error',
            summary: 'Erro',
            detail: 'Erro ao editar o produto!',
            life: 3000,
          });
          this.editProductForm.reset();
        },
      });
    }
  }


  getProductSelectedDatas(productId: string): void {
    const allProdcts = this.productAction?.productsDatas;

    if (allProdcts.length > 0) {
      const productFiltered = allProdcts.filter(
        (element) => element?.id === productId
      );

      if (productFiltered) {
        this.productSelectedDatas = productFiltered[0];

        this.editProductForm.setValue({
          name: this.productSelectedDatas?.name,
          price: this.productSelectedDatas.price,
          description: this.productSelectedDatas?.description,
        });

      }
    }
  }

  getProductDatas(): void {
    this.productService
      .getAllProduct(10, 1)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          if (response.data.length > 0) {
            this.productsDatas = response.data;
            this.productsDatas &&
              this.productsDtService.setProductsDatas(this.productsDatas);
          }
        },
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
