import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { GlobalStateService } from "../globalState.service";
import { table, getBorderCharacters } from "table";

@Component({
  selector: "employee-list",
  templateUrl: "./employee-list.component.html",
})
export class EmployeeListComponent implements OnInit {
  public employees: Employee[];
  public employeesText: string;
  public isTextFormat: boolean = false;

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string,
    private globalStateService: GlobalStateService
  ) {}

  ngOnInit() {
    if (this.globalStateService.jwtToken.length)
      this.http
        .get<Employee[]>(this.baseUrl + "api/employees", {
          headers: {
            Authorization: this.globalStateService.jwtToken,
          },
        })
        .subscribe(
          (result) => {
            this.employees = result;
          },
          (error) => console.error(error)
        );
  }

  toggleDataFormat() {
    this.isTextFormat = !this.isTextFormat;
    if (this.employees && this.employees.length && this.isTextFormat)
      this.employeesText = table(
        [
          Object.entries(this.employees[0]).map((prop) => prop[0]),
          ...this.employees.map((item) =>
            Object.entries(item).map((prop) => prop[1])
          ),
        ],
        {
          border: getBorderCharacters("ramac"),
        }
      );
  }
}

interface Employee {
  employeeName: string;
  salary: number;
  departmentNo: number;
}
