import { Component, Inject, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from "@angular/common";
import { GlobalStateService } from "../globalState.service";

@Component({
  selector: "login",
  templateUrl: "./login.component.html",
})
export class LoginComponent {
  public username: string;
  public password: string;
  public error: boolean = false;

  constructor(
    private globalStateService: GlobalStateService,
    private router: Router,
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  goBack(): void {
    this.router.navigate(["/departments"]);
  }

  onChange() {
    this.error = false;
  }

  onSubmit(): void {
    this.http
      .post(
        this.baseUrl + `api/login`,
        {
          loginUserName: this.username,
          loginPassword: this.password,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
      .subscribe(
        (result: { token?: string }) => {
          if (result && result.token) {
            this.globalStateService.jwtToken = result.token;
            this.router.navigate(["/"]);
          } else {
            this.error = true;
          }
        },
        (error) => {
          this.error = true;
        }
      );
  }
}

interface Department {
  departmentNo: number;
  departmentName: string;
  departmentLocation: string;
}
