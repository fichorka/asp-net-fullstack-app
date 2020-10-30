import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from "@angular/common";
import { GlobalStateService } from "../globalState.service";

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
  private error: boolean = false;

  public newEmployeeName: string;
  public newSalary: number;
  public newDepartmentNo: number;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    http: HttpClient,
    @Inject("BASE_URL") baseUrl: string,
    private globalStateService: GlobalStateService,
    private router: Router
  ) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit(): void {
    this.id = +this.route.snapshot.paramMap.get("id");
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
        (error) => console.error(error)
      );

    this.http
      .get<Employee>(this.baseUrl + `api/employees/${this.id}`, {
        headers: {
          Authorization: this.globalStateService.jwtToken,
        },
      })
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
    this.router.navigate(["/employees"]);
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
          this.error = error;
        }
      );
  }

  onRemove(): void {
    this.http
      .delete(this.baseUrl + `api/employees/${this.id}`, {
        headers: {
          Authorization: this.globalStateService.jwtToken,
        },
      })
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
