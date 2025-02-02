import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-toolbar-navigation',
  templateUrl: './toolbar-navigation.component.html',
  styleUrls: [],
})
export class ToolbarNavigationComponent {
  constructor(private cookie: CookieService, private router: Router){}

  handlerLogout(): void {
    this.cookie.delete('USER_INFO');
    void this.router.navigate(['/home']);

  }



}
