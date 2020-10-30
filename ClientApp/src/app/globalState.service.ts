import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class GlobalStateService {
  public jwtToken: string = "";
}
