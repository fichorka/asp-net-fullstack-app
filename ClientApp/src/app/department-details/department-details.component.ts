import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from "@angular/router";
import { Location } from "@angular/common";

@Component({
  selector: "department-details",
  templateUrl: "./department-details.component.html",
})
export class DepartmentDetailsComponent implements OnInit {
  public department: Department;
  public baseUrl: string;
  public id: number;
  public http: HttpClient;

  public newDepartmentName: string;
  public newDepartmentLocation: string;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    http: HttpClient,
    @Inject("BASE_URL") baseUrl: string
  ) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit(): void {
    this.id = +this.route.snapshot.paramMap.get("id");
    this.http
      .get<Department>(this.baseUrl + `api/departments/${this.id}`)
      .subscribe(
        (result) => {
          this.department = result;
          this.newDepartmentName = result.departmentName;
          this.newDepartmentLocation = result.departmentLocation;
        },
        (error) => console.error(error)
      );
  }

  goBack(): void {
    this.location.back();
  }

  onSubmit(): void {
    this.http
      .put(
        this.baseUrl + `api/departments/${this.id}`,
        {
          departmentNo: +this.id,
          departmentName: this.newDepartmentName,
          departmentLocation: this.newDepartmentLocation,
        },
        { headers: { "Content-Type": "application/json" } }
      )
      .subscribe(() => {
        this.goBack();
      });
  }

  onRemove(): void {
    this.http
      .delete(this.baseUrl + `api/departments/${this.id}`)
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
