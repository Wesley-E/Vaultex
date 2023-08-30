import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgForOf} from "@angular/common";
import {Organisation} from "../interfaces/organisation";
import {OrganisationsService} from "../services/organisations.service";

@Component({
  selector: 'app-organisation-table',
  templateUrl: './organisation-table.component.html',
  styleUrls: ['./organisation-table.component.css'],
  imports: [FormsModule, NgForOf],
  standalone: true
})
export class OrganisationTableComponent {
  organisations: Organisation[] = [];

  constructor(private organisationService: OrganisationsService) {}

  async ngOnInit(): Promise<void>{
    this.organisations = await this.organisationService.getAllOrganisations();
  }
}
