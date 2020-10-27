import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from "@angular/router";
import { Location } from "@angular/common";

@Component({
  selector: "employee-details",
  templateUrl: "./employee-details.component.html",
})
export class EmployeeDetailsComponent implements OnInit {
  public departments: Department[];
  public employee: Employee;
  public baseUrl: string;
  public id: number;
  public http: HttpClient;

  public newEmployeeName: string;
  public newSalary: number;
  public newDepartmentNo: number;

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
    this.http.get<Department[]>(this.baseUrl + `api/departments`).subscribe(
      (result) => {
        this.departments = result;
      },
      (error) => console.error(error)
    );

    this.http
      .get<Employee>(this.baseUrl + `api/employees/${this.id}`)
      .subscribe(
        (result) => {
          this.employee = result;
          this.newEmployeeName = result.employeeName;
          this.newSalary = result.salary;
          this.newDepartmentNo = result.departmentNo;
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
        this.baseUrl + `api/employees/${this.id}`,
        {
          employeeNo: +this.id,
          employeeName: this.newEmployeeName,
          salary: +this.newSalary,
          departmentNo: +this.newDepartmentNo,
        },
        { headers: { "Content-Type": "application/json" } }
      )
      .subscribe(() => {
        this.goBack();
      });
  }

  onRemove(): void {
    this.http
      .delete(this.baseUrl + `api/employees/${this.id}`)
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

interface Employee {
  employeeName: string;
  salary: number;
  departmentNo: number;
}
