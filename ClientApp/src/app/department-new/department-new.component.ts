import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from "@angular/router";
import { Location } from "@angular/common";

@Component({
  selector: "department-new",
  templateUrl: "./department-new.component.html",
})
export class DepartmentNewComponent {
  public department: Department;
  public baseUrl: string;
  public http: HttpClient;

  public departmentName: string;
  public departmentLocation: string;

  constructor(
    private location: Location,
    http: HttpClient,
    @Inject("BASE_URL") baseUrl: string
  ) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  goBack(): void {
    this.location.back();
  }

  onSubmit(): void {
    this.http
      .post(
        this.baseUrl + `api/departments`,
        {
          departmentName: this.departmentName,
          departmentLocation: this.departmentLocation,
        },
        { headers: { "Content-Type": "application/json" } }
      )
      .subscribe(() => {
        this.goBack();
      });
  }
}

interface Department {
  departmentNo: number;
  departmentName: string;
  departmentLocation: string;
}
