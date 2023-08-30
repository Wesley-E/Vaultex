import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {OrganisationTableComponent} from "../organisation-table/organisation-table.component";

@Component({
  selector: 'app-organisations',
  standalone: true,
  imports: [CommonModule, OrganisationTableComponent],
  templateUrl: './organisations.html',
  styleUrls: ['./organisations.component.css']
})
export class OrganisationsComponent {

}
