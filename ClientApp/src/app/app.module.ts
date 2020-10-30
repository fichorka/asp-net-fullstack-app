import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { DepartmentListComponent } from "./department-list/department-list.component";
import { DepartmentDetailsComponent } from "./department-details/department-details.component";
import { DepartmentNewComponent } from "./department-new/department-new.component";
import { EmployeeListComponent } from "./employee-list/employee-list.component";
import { EmployeeNewComponent } from "./employee-new/employee-new.component";
import { EmployeeDetailsComponent } from "./employee-details/employee-details.component";
import { LoginComponent } from "./login/login.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DepartmentListComponent,
    DepartmentDetailsComponent,
    DepartmentNewComponent,
    EmployeeListComponent,
    EmployeeNewComponent,
    EmployeeDetailsComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "departments", component: DepartmentListComponent },
      { path: "departments/new", component: DepartmentNewComponent },
      { path: "departments/:id", component: DepartmentDetailsComponent },
      { path: "employees", component: EmployeeListComponent },
      { path: "employees/new", component: EmployeeNewComponent },
      { path: "employees/:id", component: EmployeeDetailsComponent },
      { path: "login", component: LoginComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
