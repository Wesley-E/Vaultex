import { Injectable } from '@angular/core';
import {Organisation} from "../interfaces/organisation";


@Injectable({
  providedIn: 'root'
})
export class OrganisationsService {

  constructor() { }

  async getAllOrganisations(): Promise<Organisation[]> {
    const data = await fetch('http://localhost:5026/organisation')
    return await data.json() ?? []
  }
}
