import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgForOf} from "@angular/common";
import {Employee} from "../interfaces/employee";
import {EmployeesService} from "../services/employees.service";

@Component({
  selector: 'app-employee-table',
  templateUrl: './employee-table.component.html',
  styleUrls: ['./employee-table.component.css'],
  imports: [FormsModule, NgForOf],
  standalone: true
})
export class EmployeeTableComponent {
  employees: Employee[] = [];

  constructor(private employeesService: EmployeesService) {}

  async ngOnInit(): Promise<void>{
    this.employees = await this.employeesService.getAllEmployees();
  }
}
