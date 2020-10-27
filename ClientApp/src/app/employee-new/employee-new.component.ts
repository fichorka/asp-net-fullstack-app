import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from "@angular/router";
import { Location } from "@angular/common";

@Component({
  selector: "employee-new",
  templateUrl: "./employee-new.component.html",
})
export class EmployeeNewComponent implements OnInit {
  public baseUrl: string;
  public http: HttpClient;

  public departments: Department[];
  public employeeName: string;
  public salary: number;
  public departmentNo: number;

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

  ngOnInit(): void {
    this.http.get<Department[]>(this.baseUrl + `api/departments`).subscribe(
      (result) => {
        this.departments = result;
      },
      (error) => console.error(error)
    );
  }

  onSubmit(): void {
    this.http
      .post(
        this.baseUrl + `api/employees`,
        {
          employeeName: this.employeeName,
          salary: +this.salary,
          departmentNo: +this.departmentNo,
        } as Employee,
        { headers: { "Content-Type": "application/json" } }
      )
      .subscribe(() => {
        this.goBack();
      });
  }
}

interface Employee {
  employeeName: string;
  salary: number;
  departmentNo: number;
}

interface Department {
  departmentNo: number;
  departmentName: string;
  departmentLocation: string;
}
