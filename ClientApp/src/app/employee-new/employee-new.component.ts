import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from "@angular/common";
import { GlobalStateService } from "../globalState.service";

@Component({
  selector: "employee-new",
  templateUrl: "./employee-new.component.html",
})
export class EmployeeNewComponent implements OnInit {
  public baseUrl: string;
  public http: HttpClient;
  private error: boolean = false;

  public departments: Department[];
  public employeeName: string;
  public salary: number;
  public departmentNo: number;

  constructor(
    http: HttpClient,
    @Inject("BASE_URL") baseUrl: string,
    private globalStateService: GlobalStateService,
    private router: Router
  ) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  goBack(): void {
    this.router.navigate(["/employees"]);
  }

  ngOnInit(): void {
    this.http
      .get<Department[]>(this.baseUrl + `api/departments`, {
        headers: {
          Authorization: this.globalStateService.jwtToken,
        },
      })
      .subscribe(
        (result) => {
          this.departments = result;
        },
        (error) => console.log(error)
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
        {
          headers: {
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
