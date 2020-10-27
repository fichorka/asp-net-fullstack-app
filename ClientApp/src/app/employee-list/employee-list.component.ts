import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "employee-list",
  templateUrl: "./employee-list.component.html",
})
export class EmployeeListComponent {
  public employees: Employee[];

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    http.get<Employee[]>(baseUrl + "api/employees").subscribe(
      (result) => {
        this.employees = result;
      },
      (error) => console.error(error)
    );
  }
}

interface Employee {
  employeeName: string;
  salary: number;
  departmentNo: number;
}
