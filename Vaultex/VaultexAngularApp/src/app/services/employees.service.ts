import { Injectable } from '@angular/core';
import {Employee} from "../interfaces/employee";

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  constructor() { }

  async getAllEmployees(): Promise<Employee[]> {
    const data = await fetch('http://localhost:5026/employee')
    return await data.json() ?? []
  }
}


