import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from "@angular/common";
import { GlobalStateService } from "../globalState.service";

@Component({
  selector: "department-details",
  templateUrl: "./department-details.component.html",
})
export class DepartmentDetailsComponent implements OnInit {
  public department: Department;
  public baseUrl: string;
  public id: number;
  public http: HttpClient;
  private error: boolean = false;

  public newDepartmentName: string;
  public newDepartmentLocation: string;

  constructor(
    private route: ActivatedRoute,
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
      .get<Department>(this.baseUrl + `api/departments/${this.id}`, {
        headers: {
          Authorization: this.globalStateService.jwtToken,
        },
      })
      .subscribe(
        (result) => {
          this.department = result;
          this.newDepartmentName = result.departmentName;
          this.newDepartmentLocation = result.departmentLocation;
        },
        (error) => {
          console.error(error);
        }
      );
  }

  goBack(): void {
    this.router.navigate(["/departments"]);
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

  onRemove(): void {
    this.http
      .delete(this.baseUrl + `api/departments/${this.id}`, {
        headers: {
          Authorization: this.globalStateService.jwtToken,
        },
      })
      .subscribe(() => {
        this.router.navigate(["/departments"]);
      });
  }
}

interface Department {
  departmentNo: number;
  departmentName: string;
  departmentLocation: string;
}
