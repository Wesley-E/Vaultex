import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {EmployeeTableComponent} from "../employee-table/employee-table.component";

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, EmployeeTableComponent],
  templateUrl: './employees.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent {

}
