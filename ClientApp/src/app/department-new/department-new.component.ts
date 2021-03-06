import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from "@angular/common";
import { GlobalStateService } from "../globalState.service";

@Component({
  selector: "department-new",
  templateUrl: "./department-new.component.html",
})
export class DepartmentNewComponent {
  public department: Department;
  public baseUrl: string;
  public http: HttpClient;
  private error: boolean = false;

  public departmentName: string;
  public departmentLocation: string;

  constructor(
    private location: Location,
    http: HttpClient,
    @Inject("BASE_URL") baseUrl: string,
    private globalStateService: GlobalStateService,
    private router: Router
  ) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  goBack(): void {
    this.router.navigate(["/departments"]);
  }

  onSubmit(): void {
    this.http
      .post(
        this.baseUrl + `api/departments`,
        {
          departmentName: this.departmentName,
          departmentLocation: this.departmentLocation,
        },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: this.globalStateService.jwtToken,
          },
        }
      )
      .subscribe(
        () => {
          this.goBack();
        },
        (error) => {
          this.error = true;
        }
      );
  }
}

interface Department {
  departmentNo: number;
  departmentName: string;
  departmentLocation: string;
}
