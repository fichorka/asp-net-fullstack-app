import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { GlobalStateService } from "../globalState.service";

@Component({
  selector: "department-list",
  templateUrl: "./department-list.component.html",
})
export class DepartmentListComponent {
  public departments: Department[];

  constructor(
    http: HttpClient,
    @Inject("BASE_URL") baseUrl: string,
    private globalStateService: GlobalStateService
  ) {
    if (globalStateService.jwtToken.length)
      http
        .get<Department[]>(baseUrl + "api/departments", {
          headers: {
            Authorization: globalStateService.jwtToken,
          },
        })
        .subscribe(
          (result) => {
            this.departments = result;
          },
          (error) => console.error(error)
        );
  }
}

interface Department {
  departmentName: string;
  departmentLocation: string;
}
